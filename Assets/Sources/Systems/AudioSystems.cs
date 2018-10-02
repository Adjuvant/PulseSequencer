using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System.Linq;

#region Feature
public class AudioSystems : Feature
{
    public AudioSystems(Contexts contexts) : base("Audio Systems")
    {
        // order is respected 
        Add(new StartPlayingSystem(contexts));
        Add(new TickUpdateSystem(contexts));

        Add(new GeneratePulseAndPatterns(contexts));

        Add(new UpdatePulseSystem(contexts));

        Add(new IteratePulseFollowers(contexts));
        Add(new IteratePatternFollowerIndexSystem(contexts));

        Add(new TriggerStepSystem(contexts));
        Add(new StepSystem(contexts));

        Add(new UpdatePatternPulseSystem(contexts));
    }
}
#endregion

#region Global play state
public class StartPlayingSystem : IInitializeSystem
{
    readonly AudioContext audioContext;

    public StartPlayingSystem(Contexts contexts)
    {
        audioContext = contexts.audio;
    }

    public void Initialize() { audioContext.isPlaying = true; }
}
#endregion

#region Tick
public class TickUpdateSystem : IInitializeSystem, IExecuteSystem
{
    readonly AudioContext audioContext;

    public TickUpdateSystem(Contexts contexts)
    {
        audioContext = contexts.audio;
    }

    public void Initialize() { audioContext.ReplaceTick(AudioSettings.dspTime); }

    public void Execute()
    {
        if (audioContext.isPlaying)
        {
            audioContext.ReplaceTick(AudioSettings.dspTime);
        }
    }
}
#endregion

#region Pulses

public sealed class GeneratePulseAndPatterns : IInitializeSystem
{
    AudioService audioService = AudioService.singleton;

    public GeneratePulseAndPatterns(Contexts contexts)
    {
        
    }
    public void Initialize()
    {
        var pulse = audioService.CreatePulse(120, 4, 0.1f);
        //audioService.CreatePulse(97, 3, 0.1f);

        var e = audioService.CreatePattern(8, FollowType.Pulse, pulse);
        audioService.CreatePattern(RandomService.game.Int(3, 9), e);
    }
}

/// <summary>
/// Update pulse system.
/// Iterates the pulse time on the pulse enitity, based on tick.
/// </summary>
public sealed class UpdatePulseSystem : ReactiveSystem<AudioEntity>
{
    readonly AudioContext audioContext;
    readonly IGroup<AudioEntity> group;
    public UpdatePulseSystem(Contexts contexts) : base(contexts.audio)
    {
        audioContext = contexts.audio;
        group = audioContext.GetGroup(AudioMatcher.Pulse);
    }
    protected override void Execute(List<AudioEntity> entities)
    {
        var t = entities.SingleEntity<AudioEntity>();
        foreach (var e in group.GetEntities())
        {
            while (t.tick.currentTick + e.pulse.latency > e.pulse.nextPulseTime)
            {
                var thisPulseTime = e.pulse.nextPulseTime;
                var nextPulseTime = thisPulseTime + e.pulse.period;
                e.ReplacePulse(thisPulseTime, nextPulseTime, e.pulse.period,
                               e.pulse.pulsesPerBeat, e.pulse.latency);                
            }
        }
    }

    protected override bool Filter(AudioEntity entity)
    {
        return true;
    }

    protected override ICollector<AudioEntity> GetTrigger(IContext<AudioEntity> context)
    {
        return context.CreateCollector(AudioMatcher.Tick);
    }
}


#endregion

#region Patterns

public sealed class IteratePulseFollowers : ReactiveSystem<AudioEntity>
{
    readonly AudioContext audioContext;
    readonly IGroup<AudioEntity> patterns;

    public IteratePulseFollowers(Contexts contexts) : base(contexts.audio)
    {
        audioContext = contexts.audio;
        patterns = audioContext.GetGroup(AudioMatcher.Pattern);
    }

    protected override void Execute(List<AudioEntity> entities)
    {
        // TODO: match pulse trigger to the pattern. Store ref? Or link event?
        foreach (var pulseEntity in entities){
            foreach (var p in patterns.GetEntities().Where(
                         p => p.pattern.followType==FollowType.Pulse))
            {
                if (p.hasPatternFollower) continue;
                if (pulseEntity != p.pattern.pulseSource) continue;

                var stepCount = p.pattern.steps.Count;
                if (stepCount == 0) continue;

                var newStep = (p.stepIndex.value + 1) % stepCount;
                p.ReplaceStepIndex(newStep);
                p.ReplacePulseTrigger(pulseEntity.pulse.thisPulseTime);
            }
        }
    }

    protected override bool Filter(AudioEntity entity)
    {
        return entity.hasPulse && !entity.hasPattern;
    }

    protected override ICollector<AudioEntity> GetTrigger(IContext<AudioEntity> context)
    {
        return context.CreateCollector(AudioMatcher.Pulse);
    }
}

/// <summary>
/// Trigger step system.
/// Checks change in StepIndex, if step active on pattern replaces StepTriggered
/// </summary>
public sealed class TriggerStepSystem : ReactiveSystem<AudioEntity>
{
    readonly AudioContext audioContext;

    public TriggerStepSystem(Contexts contexts) : base(contexts.audio)
    {
        audioContext = contexts.audio;
    }

    protected override void Execute(List<AudioEntity> entities)
    {
        // TODO: match pulse trigger to the pattern. Store ref? Or link event?
        foreach (var p in entities)
        {
            
            if (p.pattern.steps[p.stepIndex.value].step.active)
            {
                var pulseTime = p.pulseTrigger.thisPulseTime;
                //Debug.Log("Active step: " + p.stepIndex.value);
                p.ReplaceStepTriggered(p.stepIndex.value,
                                       pulseTime);
            }
        }
    }

    protected override bool Filter(AudioEntity entity)
    {
        return entity.hasPattern && entity.pattern.steps.Count > 0;
    }

    protected override ICollector<AudioEntity> GetTrigger(IContext<AudioEntity> context)
    {
        return context.CreateCollector(AudioMatcher.StepIndex);
    }
}

/// <summary>
/// Replaces PatternPulse for each change in StepIndex.
/// Should act on all type of pattern.
/// </summary>
public sealed class StepSystem : ReactiveSystem<AudioEntity>
{
    //readonly AudioContext audioContext;

    public StepSystem(Contexts contexts) : base(contexts.audio)
    {
        //audioContext = contexts.audio;
    }

    protected override void Execute(List<AudioEntity> entities)
    {
        foreach (var p in entities)
        {
            p.attachedView.viewEntity.ReplacePatternPulse(p.stepIndex.value);
        }
    }

    protected override bool Filter(AudioEntity entity)
    {
        return entity.hasPattern && entity.pattern.steps.Count > 0 &&
                     entity.hasPulseTrigger && entity.hasAttachedView;
    }

    protected override ICollector<AudioEntity> GetTrigger(IContext<AudioEntity> context)
    {
        return context.CreateCollector(AudioMatcher.StepIndex);
    }
}


public sealed class UpdatePatternPulseSystem : ReactiveSystem<AudioEntity>
{
    //readonly AudioContext audioContext;
    //readonly IGroup<AudioEntity> group;

    public UpdatePatternPulseSystem(Contexts contexts) : base(contexts.audio)
    {
        //audioContext = contexts.audio;
        //group = audioContext.GetGroup(AudioMatcher.PatternFollower);
    }

    protected override void Execute(List<AudioEntity> entities)
    {
        // TODO: really messy! double foreach should use some events.
        foreach (var source in entities)
        {
            foreach (var e in source.followers.destinations)
                e.ReplacePulseTrigger(source.stepTriggered.pulseTime);
        }
    }

    protected override bool Filter(AudioEntity entity)
    {
        return entity.hasPattern && entity.hasFollowers;
    }

    protected override ICollector<AudioEntity> GetTrigger(IContext<AudioEntity> context)
    {
        return context.CreateCollector(AudioMatcher.StepTriggered);
    }
}
#endregion

#region Pattern Followers
public sealed class IteratePatternFollowerIndexSystem : ReactiveSystem<AudioEntity>
{
    //readonly AudioContext audioContext;
    //readonly IGroup<AudioEntity> patterns;

    public IteratePatternFollowerIndexSystem(Contexts contexts) : base(contexts.audio)
    {
        //audioContext = contexts.audio;
        //patterns = audioContext.GetGroup(AudioMatcher.Pattern);
    }

    protected override void Execute(List<AudioEntity> entities)
    {
        // TODO: match pulse trigger to the pattern. Store ref? Or link event?

        foreach (var p in entities)
        {
            var stepCount = p.pattern.steps.Count;
            if (stepCount == 0) continue;

            var newStep = (p.stepIndex.value + 1) % stepCount;
            p.ReplaceStepIndex(newStep);

        }
    }

    protected override bool Filter(AudioEntity entity)
    {
        return !entity.hasPulse && entity.hasPattern && 
                      entity.hasPatternFollower;
    }

    protected override ICollector<AudioEntity> GetTrigger(IContext<AudioEntity> context)
    {
        return context.CreateCollector(AudioMatcher.PulseTrigger);
    }
}


#endregion