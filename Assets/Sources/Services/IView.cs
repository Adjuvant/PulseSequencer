using Entitas;

public interface IView
{
    void Link(IEntity entity, IContext context);
}

public interface IViewable : IAssetEntity, IPositionEntity, IDestroyedEntity, 
 IEntity {    }

public partial class GameEntity : IViewable {}
public partial class AudioEntity : IViewable {}

