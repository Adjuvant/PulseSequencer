using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioService
{
    #region Singleton
    public static AudioService singleton = new AudioService();
    #endregion

    #region Public Constants
    public const double PeriodMinimum = 0.016; 
    public const double BpmMinimum = 0.1;
    #endregion


    Contexts _contexts;
    EntityService entityService = EntityService.singleton;

    public void Initialize(Contexts contexts)
    {
        _contexts = contexts;
    }

    internal AudioEntity CreatePulse(double bpm, uint measures, double latency)
    {
        var e = _contexts.audio.CreateEntity();
        var p = GetPeriod(bpm, measures);
        e.AddPulse(_contexts.audio.tick.currentTick,
                   _contexts.audio.tick.currentTick+p, 
                   p, 4, 0.1);
        e.AddPosition(new IntVector2(0, 0));
        e.isInteractive = true;
        e.AddAsset(Res.PulseFollower);
        return e;
    }

    internal AudioEntity CreatePattern(int size, FollowType type, AudioEntity pulseSource)
    {
        var e = _contexts.audio.CreateEntity();
        List<AudioEntity> steps = new List<AudioEntity>();
        for (int i = 0; i < size; i++){
            var s = _contexts.audio.CreateEntity();
            s.AddStep(false);
            steps.Add(s);
        }
        e.AddPattern(steps, type, pulseSource);
        e.AddStepIndex(0);
        e.AddPosition(new IntVector2(0, type == FollowType.Pulse ? 0 : -3));
        e.isInteractive = true;
        e.AddAsset(type == FollowType.Pulse ? Res.KickSampler : Res.XyloSampler);
        e.AddPulseTrigger(AudioSettings.dspTime);
        entityService.CreateButtonLayout(type == FollowType.Pulse ? 0:-3,size,e);
        return e;
    }

    internal void CreatePattern(int size, AudioEntity source){
        var e = CreatePattern(size, FollowType.Pattern, source);
        e.AddPatternFollower(source);
        var l = new List<AudioEntity>();
        l.Add(e);
        source.AddFollowers(l);
    }

    double GetPeriod(double bpm, uint pulsesPerBeat)
    {
        if (bpm < BpmMinimum)
        {
            bpm = BpmMinimum;
        }

        return 60.0 / (bpm * pulsesPerBeat);
    }
    // Pulse: float GetBPM (pulseEntity e)
    // return 60.0/(e.period*e.pulsesPerBeat);
}
