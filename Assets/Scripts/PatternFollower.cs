using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
public abstract class PatternFollower : AbstractListenerBehaviour, IStepTriggeredListener
{//    
    public abstract void OnStepTriggered(AudioEntity entity, int stepIndex, double pulseTime, AudioEntity step);
}
