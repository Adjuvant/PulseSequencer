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

        Add(new GeneratePulsesSystem(contexts));
        Add(new UpdatePulseSystem(contexts));
        //Add(new UpdatePatternPulseSystem(contexts));

        Add(new GeneratePatternSystem(contexts));

        Add(new IteratePatternIndexSystem(contexts));
        Add(new IteratePatternFollowerIndexSystem(contexts));

        Add(new TriggerStepSystem(contexts));
        Add(new StepSystem(contexts));
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

public sealed class GeneratePulsesSystem : IInitializeSystem
{
    AudioService audioService = AudioService.singleton;
    readonly AudioContext audioContext;
    public GeneratePulsesSystem(Contexts contexts)
    {
        audioContext = contexts.audio;
    }
    public void Initialize()
    {
        audioService.CreatePulse(120, 4, 0.1f);
        //audioService.CreatePulse(97, 3, 0.1f);
    }
}

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
                var _nextPulseTime = thisPulseTime + e.pulse.period;
                e.ReplacePulse(_nextPulseTime, e.pulse.period,
                               e.pulse.pulsesPerBeat, e.pulse.latency);
                e.ReplacePulseTrigger(thisPulseTime);
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

//public sealed class UpdatePatternPulseSystem : ReactiveSystem<AudioEntity>
//{
//    readonly AudioContext audioContext;
//    //readonly IGroup<AudioEntity> group;

//    public UpdatePatternPulseSystem(Contexts contexts) : base(contexts.audio)
//    {
//        audioContext = contexts.audio;
//        //group = audioContext.GetGroup(AudioMatcher.PatternFollower);
//    }

//    protected override void Execute(List<AudioEntity> entities)
//    {
//        // TODO: really messy! double foreach should use some events.
//        foreach(var source in entities)
//        {
//            foreach (var e in source.followers.destinations)
//                e.ReplacePatternTrigger(source.stepTriggered.pulseTime);
//        }
//    }

//    protected override bool Filter(AudioEntity entity)
//    {
//        return entity.hasPattern && entity.hasFollowers;
//    }

//    protected override ICollector<AudioEntity> GetTrigger(IContext<AudioEntity> context)
//    {
//        return context.CreateCollector(AudioMatcher.StepTriggered);
//    }
//}

#endregion

#region Patterns
public sealed class GeneratePatternSystem : IInitializeSystem
{
    AudioService audioService = AudioService.singleton;
    readonly AudioContext audioContext;
    public GeneratePatternSystem(Contexts contexts)
    {
        audioContext = contexts.audio;
    }
    public void Initialize()
    {
        var e = audioService.CreatePattern(8, FollowType.Pulse);
        audioService.CreatePattern(5, e);
    }
}

public sealed class IteratePatternIndexSystem : ReactiveSystem<AudioEntity>
{
    readonly AudioContext audioContext;
    readonly IGroup<AudioEntity> patterns;

    public IteratePatternIndexSystem(Contexts contexts) : base(contexts.audio)
    {
        audioContext = contexts.audio;
        patterns = audioContext.GetGroup(AudioMatcher.Pattern);
    }

    protected override void Execute(List<AudioEntity> entities)
    {
        // TODO: match pulse trigger to the pattern. Store ref? Or link event?
        var pulse = entities.SingleEntity();
        foreach (var p in patterns.GetEntities().Where(
                     p => p.pattern.followType==FollowType.Pulse))
        {
            var stepCount = p.pattern.steps.Count;
            if (stepCount == 0) break;

            var newStep = (p.stepIndex.value + 1) % stepCount;
            p.ReplaceStepIndex(newStep);
            p.ReplacePulseTrigger(pulse.pulseTrigger.thisPulseTime);
        }
    }

    protected override bool Filter(AudioEntity entity)
    {
        return entity.hasPulse && !entity.hasPattern;
    }

    protected override ICollector<AudioEntity> GetTrigger(IContext<AudioEntity> context)
    {
        return context.CreateCollector(AudioMatcher.PulseTrigger);
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
                var pulseTime = p.hasPulseTrigger ? p.pulseTrigger.thisPulseTime :
                                 p.patternTrigger.thisPulseTime;
                //Debug.Log("Active step: " + p.stepIndex.value);
                p.ReplaceStepTriggered(p.stepIndex.value,
                                       pulseTime);
            }

        }
    }

    protected override bool Filter(AudioEntity entity)
    {
        return entity.hasPattern && entity.pattern.steps.Count > 0 &&
                     (entity.hasPulseTrigger || entity.hasPatternTrigger);
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
    readonly AudioContext audioContext;

    public StepSystem(Contexts contexts) : base(contexts.audio)
    {
        audioContext = contexts.audio;
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
                     (entity.hasPulseTrigger || entity.hasPatternTrigger) && entity.hasAttachedView;
    }

    protected override ICollector<AudioEntity> GetTrigger(IContext<AudioEntity> context)
    {
        return context.CreateCollector(AudioMatcher.StepIndex);
    }
}

public sealed class IteratePatternFollowerIndexSystem : ReactiveSystem<AudioEntity>
{
    readonly AudioContext audioContext;
    //readonly IGroup<AudioEntity> patterns;

    public IteratePatternFollowerIndexSystem(Contexts contexts) : base(contexts.audio)
    {
        audioContext = contexts.audio;
        //patterns = audioContext.GetGroup(AudioMatcher.Pattern);
    }

    protected override void Execute(List<AudioEntity> entities)
    {
        // TODO: match pulse trigger to the pattern. Store ref? Or link event?

        foreach (var p in entities)
        {
            var stepCount = p.pattern.steps.Count;
            if (stepCount == 0) break;

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
        return context.CreateCollector(AudioMatcher.PatternTrigger);
    }
}


#endregion