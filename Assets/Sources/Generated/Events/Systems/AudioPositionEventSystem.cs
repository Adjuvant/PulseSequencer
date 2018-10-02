//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class AudioPositionEventSystem : Entitas.ReactiveSystem<AudioEntity> {

    readonly System.Collections.Generic.List<IAudioPositionListener> _listenerBuffer;

    public AudioPositionEventSystem(Contexts contexts) : base(contexts.audio) {
        _listenerBuffer = new System.Collections.Generic.List<IAudioPositionListener>();
    }

    protected override Entitas.ICollector<AudioEntity> GetTrigger(Entitas.IContext<AudioEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(AudioMatcher.Position)
        );
    }

    protected override bool Filter(AudioEntity entity) {
        return entity.hasPosition && entity.hasAudioPositionListener;
    }

    protected override void Execute(System.Collections.Generic.List<AudioEntity> entities) {
        foreach (var e in entities) {
            var component = e.position;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.audioPositionListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnPosition(e, component.value);
            }
        }
    }
}
