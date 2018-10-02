//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AudioEntity {

    public TickListenerComponent tickListener { get { return (TickListenerComponent)GetComponent(AudioComponentsLookup.TickListener); } }
    public bool hasTickListener { get { return HasComponent(AudioComponentsLookup.TickListener); } }

    public void AddTickListener(System.Collections.Generic.List<ITickListener> newValue) {
        var index = AudioComponentsLookup.TickListener;
        var component = CreateComponent<TickListenerComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTickListener(System.Collections.Generic.List<ITickListener> newValue) {
        var index = AudioComponentsLookup.TickListener;
        var component = CreateComponent<TickListenerComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTickListener() {
        RemoveComponent(AudioComponentsLookup.TickListener);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class AudioMatcher {

    static Entitas.IMatcher<AudioEntity> _matcherTickListener;

    public static Entitas.IMatcher<AudioEntity> TickListener {
        get {
            if (_matcherTickListener == null) {
                var matcher = (Entitas.Matcher<AudioEntity>)Entitas.Matcher<AudioEntity>.AllOf(AudioComponentsLookup.TickListener);
                matcher.componentNames = AudioComponentsLookup.componentNames;
                _matcherTickListener = matcher;
            }

            return _matcherTickListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AudioEntity {

    public void AddTickListener(ITickListener value) {
        var listeners = hasTickListener
            ? tickListener.value
            : new System.Collections.Generic.List<ITickListener>();
        listeners.Add(value);
        ReplaceTickListener(listeners);
    }

    public void RemoveTickListener(ITickListener value, bool removeComponentWhenEmpty = true) {
        var listeners = tickListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveTickListener();
        } else {
            ReplaceTickListener(listeners);
        }
    }
}
