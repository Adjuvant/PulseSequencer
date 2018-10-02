//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AudioEntity {

    public StepIndex stepIndex { get { return (StepIndex)GetComponent(AudioComponentsLookup.StepIndex); } }
    public bool hasStepIndex { get { return HasComponent(AudioComponentsLookup.StepIndex); } }

    public void AddStepIndex(int newValue) {
        var index = AudioComponentsLookup.StepIndex;
        var component = CreateComponent<StepIndex>(index);
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceStepIndex(int newValue) {
        var index = AudioComponentsLookup.StepIndex;
        var component = CreateComponent<StepIndex>(index);
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveStepIndex() {
        RemoveComponent(AudioComponentsLookup.StepIndex);
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

    static Entitas.IMatcher<AudioEntity> _matcherStepIndex;

    public static Entitas.IMatcher<AudioEntity> StepIndex {
        get {
            if (_matcherStepIndex == null) {
                var matcher = (Entitas.Matcher<AudioEntity>)Entitas.Matcher<AudioEntity>.AllOf(AudioComponentsLookup.StepIndex);
                matcher.componentNames = AudioComponentsLookup.componentNames;
                _matcherStepIndex = matcher;
            }

            return _matcherStepIndex;
        }
    }
}