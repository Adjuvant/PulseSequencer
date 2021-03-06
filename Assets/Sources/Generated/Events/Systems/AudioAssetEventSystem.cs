//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class AudioAssetEventSystem : Entitas.ReactiveSystem<AudioEntity> {

    readonly Entitas.IGroup<AudioEntity> _listeners;
    readonly System.Collections.Generic.List<AudioEntity> _entityBuffer;
    readonly System.Collections.Generic.List<IAudioAssetListener> _listenerBuffer;

    public AudioAssetEventSystem(Contexts contexts) : base(contexts.audio) {
        _listeners = contexts.audio.GetGroup(AudioMatcher.AudioAssetListener);
        _entityBuffer = new System.Collections.Generic.List<AudioEntity>();
        _listenerBuffer = new System.Collections.Generic.List<IAudioAssetListener>();
    }

    protected override Entitas.ICollector<AudioEntity> GetTrigger(Entitas.IContext<AudioEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(AudioMatcher.Asset)
        );
    }

    protected override bool Filter(AudioEntity entity) {
        return entity.hasAsset;
    }

    protected override void Execute(System.Collections.Generic.List<AudioEntity> entities) {
        foreach (var e in entities) {
            var component = e.asset;
            foreach (var listenerEntity in _listeners.GetEntities(_entityBuffer)) {
                _listenerBuffer.Clear();
                _listenerBuffer.AddRange(listenerEntity.audioAssetListener.value);
                foreach (var listener in _listenerBuffer) {
                    listener.OnAsset(e, component.value);
                }
            }
        }
    }
}
