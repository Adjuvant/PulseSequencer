using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public abstract class AbstractListenerBehaviour : MonoBehaviour, IEventListener {
    public abstract void RegisterListeners(IEntity entity);
}
