using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;

public class GameController : MonoBehaviour {

    public Services services = Services.singleton;

    Systems _systems;

    void Awake()
    {
        var contexts = Contexts.sharedInstance;
        services.Initialize(contexts, this);
        _systems = new Systems(contexts);
    }

    void Start()
    {
        _systems.Initialize();
    }

    void Update()
    {
        _systems.Execute();
        _systems.Cleanup();
    }

    void OnDestroy()
    {
        _systems.TearDown();
    }
}
