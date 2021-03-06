//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AudioEntity {

    public StepComponent step { get { return (StepComponent)GetComponent(AudioComponentsLookup.Step); } }
    public bool hasStep { get { return HasComponent(AudioComponentsLookup.Step); } }

    public void AddStep(bool newActive) {
        var index = AudioComponentsLookup.Step;
        var component = CreateComponent<StepComponent>(index);
        component.active = newActive;
        AddComponent(index, component);
    }

    public void ReplaceStep(bool newActive) {
        var index = AudioComponentsLookup.Step;
        var component = CreateComponent<StepComponent>(index);
        component.active = newActive;
        ReplaceComponent(index, component);
    }

    public void RemoveStep() {
        RemoveComponent(AudioComponentsLookup.Step);
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

    static Entitas.IMatcher<AudioEntity> _matcherStep;

    public static Entitas.IMatcher<AudioEntity> Step {
        get {
            if (_matcherStep == null) {
                var matcher = (Entitas.Matcher<AudioEntity>)Entitas.Matcher<AudioEntity>.AllOf(AudioComponentsLookup.Step);
                matcher.componentNames = AudioComponentsLookup.componentNames;
                _matcherStep = matcher;
            }

            return _matcherStep;
        }
    }
}
