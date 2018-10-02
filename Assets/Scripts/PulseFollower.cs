using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Entitas;

public class PulseFollower : AbstractListenerBehaviour, IPulseListener {

    [SerializeField] Text data1;
    [SerializeField] Text data2;

	public void OnEnable()
	{
        data1 = GameObject.Find("data1").GetComponent<Text>();
        data2 = GameObject.Find("data2").GetComponent<Text>();
	}

    public override void RegisterListeners(IEntity entity)
    {
        var e = (AudioEntity)entity;
        e.AddPulseListener(this);
    }

    public void OnPulse(AudioEntity entity, double thisPulseTime, double nextPulseTime, 
                        double period, uint pulsesPerBeat, double latency)
    {
        if (data1)
            data1.text = entity.ToString();
        if (data2)
            data2.text = thisPulseTime.ToString();
    }
}
