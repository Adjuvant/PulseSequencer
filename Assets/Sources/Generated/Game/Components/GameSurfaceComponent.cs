//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public SurfaceComponent surface { get { return (SurfaceComponent)GetComponent(GameComponentsLookup.Surface); } }
    public bool hasSurface { get { return HasComponent(GameComponentsLookup.Surface); } }

    public void AddSurface(System.Collections.Generic.List<GameEntity> newHolders) {
        var index = GameComponentsLookup.Surface;
        var component = CreateComponent<SurfaceComponent>(index);
        component.holders = newHolders;
        AddComponent(index, component);
    }

    public void ReplaceSurface(System.Collections.Generic.List<GameEntity> newHolders) {
        var index = GameComponentsLookup.Surface;
        var component = CreateComponent<SurfaceComponent>(index);
        component.holders = newHolders;
        ReplaceComponent(index, component);
    }

    public void RemoveSurface() {
        RemoveComponent(GameComponentsLookup.Surface);
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

    static Entitas.IMatcher<GameEntity> _matcherSurface;

    public static Entitas.IMatcher<GameEntity> Surface {
        get {
            if (_matcherSurface == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Surface);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSurface = matcher;
            }

            return _matcherSurface;
        }
    }
}
