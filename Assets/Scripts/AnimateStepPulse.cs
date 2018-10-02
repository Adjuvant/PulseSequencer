using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace ButtonBehaviours
{
    [RequireComponent(typeof(Animator))]
    public class AnimateStepPulse : AbstractListenerBehaviour,IStepActionListener
    {
        Animator _animator;
        [SerializeField] private string _triggerName;

        public void OnStepAction(GameEntity entity)
        {
            // Debug.Log(this);
            _animator.SetTrigger(_triggerName);
        }

        public override void RegisterListeners(IEntity entity)
        {
            var e = (GameEntity)entity;
            e.AddStepActionListener(this);
        }
        // Use this for initialization
        void Start()
        {
            _animator = GetComponent<Animator>();
        }
    }
}
