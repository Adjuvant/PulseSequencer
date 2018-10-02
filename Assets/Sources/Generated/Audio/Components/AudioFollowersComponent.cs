//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AudioEntity {

    public FollowersComponent followers { get { return (FollowersComponent)GetComponent(AudioComponentsLookup.Followers); } }
    public bool hasFollowers { get { return HasComponent(AudioComponentsLookup.Followers); } }

    public void AddFollowers(System.Collections.Generic.List<AudioEntity> newDestinations) {
        var index = AudioComponentsLookup.Followers;
        var component = CreateComponent<FollowersComponent>(index);
        component.destinations = newDestinations;
        AddComponent(index, component);
    }

    public void ReplaceFollowers(System.Collections.Generic.List<AudioEntity> newDestinations) {
        var index = AudioComponentsLookup.Followers;
        var component = CreateComponent<FollowersComponent>(index);
        component.destinations = newDestinations;
        ReplaceComponent(index, component);
    }

    public void RemoveFollowers() {
        RemoveComponent(AudioComponentsLookup.Followers);
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

    static Entitas.IMatcher<AudioEntity> _matcherFollowers;

    public static Entitas.IMatcher<AudioEntity> Followers {
        get {
            if (_matcherFollowers == null) {
                var matcher = (Entitas.Matcher<AudioEntity>)Entitas.Matcher<AudioEntity>.AllOf(AudioComponentsLookup.Followers);
                matcher.componentNames = AudioComponentsLookup.componentNames;
                _matcherFollowers = matcher;
            }

            return _matcherFollowers;
        }
    }
}
