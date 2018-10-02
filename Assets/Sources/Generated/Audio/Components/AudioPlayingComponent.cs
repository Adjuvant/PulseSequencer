//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AudioContext {

    public AudioEntity playingEntity { get { return GetGroup(AudioMatcher.Playing).GetSingleEntity(); } }

    public bool isPlaying {
        get { return playingEntity != null; }
        set {
            var entity = playingEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isPlaying = true;
                } else {
                    entity.Destroy();
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AudioEntity {

    static readonly PlayingComponent playingComponent = new PlayingComponent();

    public bool isPlaying {
        get { return HasComponent(AudioComponentsLookup.Playing); }
        set {
            if (value != isPlaying) {
                var index = AudioComponentsLookup.Playing;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : playingComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<AudioEntity> _matcherPlaying;

    public static Entitas.IMatcher<AudioEntity> Playing {
        get {
            if (_matcherPlaying == null) {
                var matcher = (Entitas.Matcher<AudioEntity>)Entitas.Matcher<AudioEntity>.AllOf(AudioComponentsLookup.Playing);
                matcher.componentNames = AudioComponentsLookup.componentNames;
                _matcherPlaying = matcher;
            }

            return _matcherPlaying;
        }
    }
}