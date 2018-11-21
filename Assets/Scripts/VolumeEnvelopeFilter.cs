using System;
using UnityEngine;
using System.Runtime.InteropServices;


/// <summary>
/// Volume envelope filter act in the OnAudioFilerRead
/// TODO: replace with native plugin instance
/// </summary>
public class VolumeEnvelopeFilter : MonoBehaviour
{
    public bool Enabled;

    [SerializeField, Range(0f, 1f)] public double _attackDuration = 0;
    [SerializeField, Range(0f, 1f)] public double _sustainDuration = 1;
    [SerializeField, Range(0f, 1f)] public double _releaseDuration = 0.1;
    private IntPtr _envelopePtr = IntPtr.Zero;
    private AudioSource _audioSource;

    [DllImport("VolumeEnvelopeNative")]
    private static extern IntPtr VolumeEnvelope_New(double sampleDuration);

    [DllImport("VolumeEnvelopeNative")]
    private static extern void VolumeEnvelope_Delete(IntPtr env);

    [DllImport("VolumeEnvelopeNative")]
    private static extern void VolumeEnvelope_SetEnvelope(IntPtr env, double startTime,
        double attackDuration, double sustainDuration, double releaseDuration);

    [DllImport("VolumeEnvelopeNative")]
    private static extern bool VolumeEnvelope_ProcessBuffer(IntPtr env, [In, Out] float[] buffer, int numSamples,
        int numChannels, double dspTime);

    [DllImport("VolumeEnvelopeNative")]
    private static extern bool VolumeEnvelope_IsAvailable(IntPtr env);

    private void OnEnable()
    {
        _envelopePtr = VolumeEnvelope_New(1.0 / AudioSettings.outputSampleRate);

        _audioSource = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        if (_envelopePtr != IntPtr.Zero)
        {
            VolumeEnvelope_Delete(_envelopePtr);
            _envelopePtr = IntPtr.Zero;
        }
    }

    public void Trigger(double triggerTime)
    {
        if (_envelopePtr != IntPtr.Zero && VolumeEnvelope_IsAvailable(_envelopePtr))
        {
            VolumeEnvelope_SetEnvelope(_envelopePtr, AudioSettings.dspTime, _attackDuration, _sustainDuration, _releaseDuration);
        }
    }

    private void OnAudioFilterRead(float[] buffer, int numChannels)
    {
        // if not enabled, don't do any attenuation
        if (!Enabled)
        {
            return;
        }

        if (_envelopePtr != IntPtr.Zero)
        {
            VolumeEnvelope_ProcessBuffer(_envelopePtr, buffer, buffer.Length, numChannels, AudioSettings.dspTime);
        }
    }
}
