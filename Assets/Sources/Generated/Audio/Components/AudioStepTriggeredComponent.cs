//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AudioEntity {

    public StepTriggered stepTriggered { get { return (StepTriggered)GetComponent(AudioComponentsLookup.StepTriggered); } }
    public bool hasStepTriggered { get { return HasComponent(AudioComponentsLookup.StepTriggered); } }

    public void AddStepTriggered(int newStepIndex, double newPulseTime, AudioEntity newStep) {
        var index = AudioComponentsLookup.StepTriggered;
        var component = CreateComponent<StepTriggered>(index);
        component.stepIndex = newStepIndex;
        component.pulseTime = newPulseTime;
        component.step = newStep;
        AddComponent(index, component);
    }

    public void ReplaceStepTriggered(int newStepIndex, double newPulseTime, AudioEntity newStep) {
        var index = AudioComponentsLookup.StepTriggered;
        var component = CreateComponent<StepTriggered>(index);
        component.stepIndex = newStepIndex;
        component.pulseTime = newPulseTime;
        component.step = newStep;
        ReplaceComponent(index, component);
    }

    public void RemoveStepTriggered() {
        RemoveComponent(AudioComponentsLookup.StepTriggered);
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

    static Entitas.IMatcher<AudioEntity> _matcherStepTriggered;

    public static Entitas.IMatcher<AudioEntity> StepTriggered {
        get {
            if (_matcherStepTriggered == null) {
                var matcher = (Entitas.Matcher<AudioEntity>)Entitas.Matcher<AudioEntity>.AllOf(AudioComponentsLookup.StepTriggered);
                matcher.componentNames = AudioComponentsLookup.componentNames;
                _matcherStepTriggered = matcher;
            }

            return _matcherStepTriggered;
        }
    }
}
