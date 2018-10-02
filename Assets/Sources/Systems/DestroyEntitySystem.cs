using System.Collections.Generic;
using Entitas;

public interface IDestroyEntity : IEntity, IViewable, IDestroyedEntity { }

// tell the compiler that our context-specific entities implement IDestroyed
public partial class GameEntity : IDestroyEntity { }
public partial class AudioEntity : IDestroyEntity { }


public sealed class DestroyEntitySystem : MultiReactiveSystem<IDestroyEntity, Contexts> 
{
    public DestroyEntitySystem(Contexts contexts) : base(contexts) 
    {
    }

    protected override ICollector[] GetTrigger(Contexts contexts)
    {
        return new ICollector[] {
            contexts.game.CreateCollector(GameMatcher.Destroyed),
            contexts.audio.CreateCollector(AudioMatcher.Destroyed),
        };
    }

    protected override bool Filter(IDestroyEntity entity)
    {
        return entity.isDestroyed;
    }

    protected override void Execute(List<IDestroyEntity> entities)
    {
        foreach (var e in entities)
        {            
            // now we can access the ViewComponent and the DestroyedComponent
            e.Destroy();
        }
    }
}
