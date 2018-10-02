using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Entitas.CodeGeneration.Attributes;

#region Global play state

[Audio, Unique, Event(EventTarget.Any)]
public class PlayingComponent : IComponent { }

#endregion

#region Tick
[Audio, Unique, Event(EventTarget.Any)]
public class TickComponent : IComponent
{
    public double currentTick;
}

#endregion 

#region Pulses
/// <summary>
/// Pulse component. Used only on pulse entities.
/// </summary>
[Audio,Event(EventTarget.Any)]
public class PulseComponent : IComponent
{
    public double thisPulseTime;
    public double nextPulseTime;
    public double period;
    public uint pulsesPerBeat;
    public double latency;
}

[Audio]
public class PatternFollowerComponent : IComponent
{
    public AudioEntity source;
}

[Audio]
public class FollowersComponent : IComponent
{
    public List<AudioEntity> destinations;
}

[Audio, Event(EventTarget.Self)]
public class PulseTriggerComponent : IComponent
{
    public double thisPulseTime;
}
#endregion

#region Patterns
public enum FollowType
{
    Pulse,
    Pattern
}

[Audio]
public class AttachedView : IComponent
{
    public GameEntity viewEntity;
}

[Audio]
public class StepComponent : IComponent
{
    public bool active;
}

[Audio]
public class Pattern : IComponent
{    
    public List<AudioEntity> steps;
    public FollowType followType;
    public AudioEntity pulseSource;
}

[Audio]
public class StepIndex : IComponent
{
    public int value;
}

[Audio, Event(EventTarget.Self)]
public class StepTriggered : IComponent
{
    public int stepIndex;
    public double pulseTime;
}
#endregion
