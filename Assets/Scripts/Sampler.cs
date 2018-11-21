using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Cycles through specified sounds each time the assigned pattern steps
/// </summary>
public class Sampler : PatternFollower
{
    /// <summary>
    /// The collection of samples
    /// </summary>
    public List<Sample> Samples = new List<Sample>();


    /// <summary>
    /// The AudioSource prefab that should be used as a template for voice instances
    /// </summary>
    [SerializeField] private AudioSource _audioSourcePrefab;

    /// <summary>
    /// How many samples should play at once?
    /// If the number of requested samples exceeds this, voice stealing happens (first in, first out)
    /// </summary>
    [SerializeField] private uint _voices = 2;

    private readonly List<AudioSource> _audioSources = new List<AudioSource>();

    private int _currentSampleIndex = 0;

    private int _currentAudioSourceIndex = 0;

    private void Awake()
    {
        for (var i = 0; i < _voices; i++)
        {
            var audioSource = Instantiate(_audioSourcePrefab);
            audioSource.transform.parent = transform;
            audioSource.transform.localPosition = Vector3.zero;

            _audioSources.Add(audioSource);
        }
    }

    public override void RegisterListeners(IEntity entity)
    {
        var e = (AudioEntity)entity;
        e.AddStepTriggeredListener(this);
    }

    public override void OnStepTriggered(AudioEntity entity, int stepIndex, double pulseTime, AudioEntity step)
    {
        if (Samples.Count == 0)
        {
            return;
        }

        var currentSample = Samples[_currentSampleIndex];
        _currentSampleIndex = (_currentSampleIndex + 1) % Samples.Count;

        var volume = step.hasVolume ? step.volume.value : 1f;
        var pitch = step.hasPitch ? step.pitch.value : 0f;
        var pulsePeriod = entity.pattern.pulseSource.hasPulse ?
                            entity.pattern.pulseSource.pulse.period : // Tempo pattern
                            entity.pattern.pulseSource.pattern.pulseSource.pulse.period; // Pattern follower
        var offset = step.hasOffset ? step.offset.value * pulsePeriod : 0f;

        currentSample.Envelope.Volume = volume;
        currentSample.Pitch = pitch;
        currentSample.Envelope.Offset = offset;
        // if suspended, keep counting sample indices in order to keep in phase
        //if (Suspended)
        //{
        //    return;
        //}

        var currentAudioSource = _audioSources[_currentAudioSourceIndex];
        _currentAudioSourceIndex = (_currentAudioSourceIndex + 1) % _audioSources.Count;

        var envelopeFilter = currentAudioSource.GetComponent<VolumeEnvelopeFilter>();

        if (envelopeFilter != null)
        {
            envelopeFilter.Enabled = currentSample.Envelope.Enabled;

            if (currentSample.Envelope.Enabled)
            {
                envelopeFilter._attackDuration = currentSample.Envelope.AttackTime;
                envelopeFilter._sustainDuration = currentSample.Envelope.SustainTime;
                envelopeFilter._releaseDuration = currentSample.Envelope.ReleaseTime;
                envelopeFilter.Trigger(pulseTime);
            }
        }
        currentAudioSource.volume = currentSample.Volume;
        currentAudioSource.clip = currentSample.Clip;
        currentAudioSource.pitch = currentSample.Pitch;
        //currentAudioSource.timeSamples = currentSample.Offset;
        currentAudioSource.PlayScheduled(pulseTime + offset);
    }
}