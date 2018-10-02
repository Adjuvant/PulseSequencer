//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public HolderComponent holder { get { return (HolderComponent)GetComponent(GameComponentsLookup.Holder); } }
    public bool hasHolder { get { return HasComponent(GameComponentsLookup.Holder); } }

    public void AddHolder(System.Collections.Generic.List<GameEntity> newItems, HolderArrangement newArrangement) {
        var index = GameComponentsLookup.Holder;
        var component = CreateComponent<HolderComponent>(index);
        component.items = newItems;
        component.arrangement = newArrangement;
        AddComponent(index, component);
    }

    public void ReplaceHolder(System.Collections.Generic.List<GameEntity> newItems, HolderArrangement newArrangement) {
        var index = GameComponentsLookup.Holder;
        var component = CreateComponent<HolderComponent>(index);
        component.items = newItems;
        component.arrangement = newArrangement;
        ReplaceComponent(index, component);
    }

    public void RemoveHolder() {
        RemoveComponent(GameComponentsLookup.Holder);
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

    static Entitas.IMatcher<GameEntity> _matcherHolder;

    public static Entitas.IMatcher<GameEntity> Holder {
        get {
            if (_matcherHolder == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Holder);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherHolder = matcher;
            }

            return _matcherHolder;
        }
    }
}