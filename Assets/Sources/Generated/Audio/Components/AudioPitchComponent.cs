//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AudioEntity {

    public PitchComponent pitch { get { return (PitchComponent)GetComponent(AudioComponentsLookup.Pitch); } }
    public bool hasPitch { get { return HasComponent(AudioComponentsLookup.Pitch); } }

    public void AddPitch(float newValue) {
        var index = AudioComponentsLookup.Pitch;
        var component = CreateComponent<PitchComponent>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePitch(float newValue) {
        var index = AudioComponentsLookup.Pitch;
        var component = CreateComponent<PitchComponent>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePitch() {
        RemoveComponent(AudioComponentsLookup.Pitch);
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

    static Entitas.IMatcher<AudioEntity> _matcherPitch;

    public static Entitas.IMatcher<AudioEntity> Pitch {
        get {
            if (_matcherPitch == null) {
                var matcher = (Entitas.Matcher<AudioEntity>)Entitas.Matcher<AudioEntity>.AllOf(AudioComponentsLookup.Pitch);
                matcher.componentNames = AudioComponentsLookup.componentNames;
                _matcherPitch = matcher;
            }

            return _matcherPitch;
        }
    }
}