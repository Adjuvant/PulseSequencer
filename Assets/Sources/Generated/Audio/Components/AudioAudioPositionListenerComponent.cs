//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AudioEntity {

    public AudioPositionListenerComponent audioPositionListener { get { return (AudioPositionListenerComponent)GetComponent(AudioComponentsLookup.AudioPositionListener); } }
    public bool hasAudioPositionListener { get { return HasComponent(AudioComponentsLookup.AudioPositionListener); } }

    public void AddAudioPositionListener(System.Collections.Generic.List<IAudioPositionListener> newValue) {
        var index = AudioComponentsLookup.AudioPositionListener;
        var component = CreateComponent<AudioPositionListenerComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceAudioPositionListener(System.Collections.Generic.List<IAudioPositionListener> newValue) {
        var index = AudioComponentsLookup.AudioPositionListener;
        var component = CreateComponent<AudioPositionListenerComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveAudioPositionListener() {
        RemoveComponent(AudioComponentsLookup.AudioPositionListener);
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

    static Entitas.IMatcher<AudioEntity> _matcherAudioPositionListener;

    public static Entitas.IMatcher<AudioEntity> AudioPositionListener {
        get {
            if (_matcherAudioPositionListener == null) {
                var matcher = (Entitas.Matcher<AudioEntity>)Entitas.Matcher<AudioEntity>.AllOf(AudioComponentsLookup.AudioPositionListener);
                matcher.componentNames = AudioComponentsLookup.componentNames;
                _matcherAudioPositionListener = matcher;
            }

            return _matcherAudioPositionListener;
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

    public void AddAudioPositionListener(IAudioPositionListener value) {
        var listeners = hasAudioPositionListener
            ? audioPositionListener.value
            : new System.Collections.Generic.List<IAudioPositionListener>();
        listeners.Add(value);
        ReplaceAudioPositionListener(listeners);
    }

    public void RemoveAudioPositionListener(IAudioPositionListener value, bool removeComponentWhenEmpty = true) {
        var listeners = audioPositionListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveAudioPositionListener();
        } else {
            ReplaceAudioPositionListener(listeners);
        }
    }
}