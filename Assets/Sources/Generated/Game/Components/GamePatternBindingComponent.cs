//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public PatternBindingComponent patternBinding { get { return (PatternBindingComponent)GetComponent(GameComponentsLookup.PatternBinding); } }
    public bool hasPatternBinding { get { return HasComponent(GameComponentsLookup.PatternBinding); } }

    public void AddPatternBinding(AudioEntity newEntity) {
        var index = GameComponentsLookup.PatternBinding;
        var component = CreateComponent<PatternBindingComponent>(index);
        component.entity = newEntity;
        AddComponent(index, component);
    }

    public void ReplacePatternBinding(AudioEntity newEntity) {
        var index = GameComponentsLookup.PatternBinding;
        var component = CreateComponent<PatternBindingComponent>(index);
        component.entity = newEntity;
        ReplaceComponent(index, component);
    }

    public void RemovePatternBinding() {
        RemoveComponent(GameComponentsLookup.PatternBinding);
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
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherPatternBinding;

    public static Entitas.IMatcher<GameEntity> PatternBinding {
        get {
            if (_matcherPatternBinding == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.PatternBinding);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPatternBinding = matcher;
            }

            return _matcherPatternBinding;
        }
    }
}
