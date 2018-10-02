using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System.Linq;

public class InputSystems : Feature
{
    public InputSystems(Contexts contexts) : base("Input Systems")
    {
        // order is respected 
        Add(new ProcessInputSystem(contexts));
    }
}

public sealed class ButtonInputSystem { }

public sealed class ProcessInputSystem : IExecuteSystem, ICleanupSystem
{

    readonly Contexts _contexts;
    readonly IGroup<InputEntity> _inputs;

    // get a reference to the group of entities with InputComponent attached 
    public ProcessInputSystem(Contexts contexts)
    {
        _contexts = contexts;
        _inputs = _contexts.input.GetGroup(InputMatcher.Input);
    }

    // this runs early every frame (defined by its order in GameController.cs)
    public void Execute()
    {
        foreach (var e in _inputs.GetEntities())
        {
            foreach (var ge in _contexts.game.GetEntitiesWithPosition(
                new IntVector2(e.input.x, e.input.y)).Where(ge => ge.isInteractive))
            {

                if (ge.hasButton)
                    ge.ReplaceButton(
                        ge.button.state == ButtonState.Off ?
                        ButtonState.On : ButtonState.Off
                    );
            }
        }
    }

    // all other systems are done so we can destroy the input entities we created
    public void Cleanup()
    {
        // group.GetEntities() always provides an up-to-date list
        foreach (var e in _inputs.GetEntities())
        {
            e.Destroy();
        }
    }
}