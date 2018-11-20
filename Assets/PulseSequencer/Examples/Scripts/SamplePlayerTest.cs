using System;
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

[RequireComponent(typeof(AudioSource))]
public class SamplePlayerTest : MonoBehaviour
{
    [SerializeField] private bool _debugPlay;
    [SerializeField] private AudioClip _clip;
    [SerializeField, Range(0f, 1f)] private double _attackDuration = 0;
    [SerializeField, Range(0f, 1f)] private double _sustainDuration = 1;
    [SerializeField, Range(0f, 1f)] private double _releaseDuration = 0.1;

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
        _envelopePtr = VolumeEnvelope_New(1.0/AudioSettings.outputSampleRate);

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

    private void Update()
    {
        if (_debugPlay)
        {
            if (_envelopePtr != IntPtr.Zero && VolumeEnvelope_IsAvailable(_envelopePtr))
            {
                VolumeEnvelope_SetEnvelope(_envelopePtr, AudioSettings.dspTime, _attackDuration, _sustainDuration, _releaseDuration);
                _audioSource.clip = _clip;
                _audioSource.timeSamples = 0;
                _audioSource.Play();
            }
            _debugPlay = false;
        }
    }

    private void OnAudioFilterRead(float[] buffer, int numChannels)
    {
        if (_envelopePtr != IntPtr.Zero)
        {
            VolumeEnvelope_ProcessBuffer(_envelopePtr, buffer, buffer.Length, numChannels, AudioSettings.dspTime);
        }
    }
}
