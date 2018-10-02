//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AudioEntity {

    public PatternTriggerComponent patternTrigger { get { return (PatternTriggerComponent)GetComponent(AudioComponentsLookup.PatternTrigger); } }
    public bool hasPatternTrigger { get { return HasComponent(AudioComponentsLookup.PatternTrigger); } }

    public void AddPatternTrigger(double newThisPulseTime) {
        var index = AudioComponentsLookup.PatternTrigger;
        var component = CreateComponent<PatternTriggerComponent>(index);
        component.thisPulseTime = newThisPulseTime;
        AddComponent(index, component);
    }

    public void ReplacePatternTrigger(double newThisPulseTime) {
        var index = AudioComponentsLookup.PatternTrigger;
        var component = CreateComponent<PatternTriggerComponent>(index);
        component.thisPulseTime = newThisPulseTime;
        ReplaceComponent(index, component);
    }

    public void RemovePatternTrigger() {
        RemoveComponent(AudioComponentsLookup.PatternTrigger);
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

    static Entitas.IMatcher<AudioEntity> _matcherPatternTrigger;

    public static Entitas.IMatcher<AudioEntity> PatternTrigger {
        get {
            if (_matcherPatternTrigger == null) {
                var matcher = (Entitas.Matcher<AudioEntity>)Entitas.Matcher<AudioEntity>.AllOf(AudioComponentsLookup.PatternTrigger);
                matcher.componentNames = AudioComponentsLookup.componentNames;
                _matcherPatternTrigger = matcher;
            }

            return _matcherPatternTrigger;
        }
    }
}
