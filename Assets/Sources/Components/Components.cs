using Entitas;
using UnityEngine;
using Entitas.CodeGeneration.Attributes;
using System.Collections.Generic;

// View events from FNGames example
public interface IEventListener
{
    void RegisterListeners(IEntity entity);
}

// Reactive UI from mzaks example
public interface IButtonListener
{
    void ButtonChanged(ButtonState value);
}

[Game]
public class ButtonListenerComponent : IComponent
{
    public IButtonListener listener;
}

// Match-one data types
[Input]
public sealed class InputComponent : IComponent
{
    public int x;
    public int y;
}

[Input]
public sealed class MouseUpComponent : IComponent{
    public GameEntity origin;
}

[Game, Audio]
public sealed class InteractiveComponent : IComponent
{
}


[Game, Audio, Event(EventTarget.Self)]
public sealed class DestroyedComponent : IComponent
{
}

[Game, Audio, Event(EventTarget.Self)]
public sealed class PositionComponent : IComponent
{
    public IntVector2 value;
}

[Game]
public sealed class ButtonComponent : IComponent
{
    // Int not bool beacuse
    // 0: off
    // 1: on
    // 2: currently pressed
    public ButtonState state;
}

public enum ButtonState
{
    Off = 0,
    On = 1,
    Pressed = 2
}

public enum HolderArrangement
{
    Linear//,Grid,Radial
}

[Game] // A surface holds a variety of data
public sealed class SurfaceComponent : IComponent
{
    public List<GameEntity> holders; // children
}

[Game] // A holder holds a variety of items
public sealed class HolderComponent : IComponent
{
    public List<GameEntity> items; // children
    public HolderArrangement arrangement;
}

[Game]
public sealed class ParentComponent : IComponent{
    public GameEntity entity;
}

[Game]
public sealed class ItemIndexComponent : IComponent
{
    public int value;
}

[Game] // Attaches to button holder to send and receive changes.
public sealed class PatternBindingComponent : IComponent
{
    public AudioEntity entity;
}

[Game]
public sealed class PatternPulseComponent : IComponent
{
    public int step;
}

[Game, Event(EventTarget.Self)]
public sealed class StepActionComponent : IComponent{}