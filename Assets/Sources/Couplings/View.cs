using Entitas;
using Entitas.Unity;
using UnityEngine;

public class View : MonoBehaviour, IView, IPositionListener, IDestroyedListener {

    public virtual void Link(IEntity entity, IContext context) {
        gameObject.Link(entity, context);
        //var e = (GameEntity)entity;


        //var pos = e.position.value;
        //transform.localPosition = new Vector3(pos.x, pos.y);

        // This could be added to view service. But currently I like it here.
        // Because this view is the thing responsible for collecting behaviours.
        // This design allows creation of behaviours around a prefab that 
        // respond to behavioural catagories.
        var eventListeners = GetComponents<IEventListener>();
        foreach(var listener in eventListeners) {
            listener.RegisterListeners(entity);
        }
    }

    public virtual void OnDestroyed(AudioEntity entity)
    {
        destroy();
    }

    public virtual void OnDestroyed(GameEntity entity) {
        destroy();
    }

    public void OnPosition(AudioEntity entity, IntVector2 value)
    {
        OnPosition(value);
    }

    public void OnPosition(GameEntity entity, IntVector2 value)
    {
        OnPosition(value);
    }

    private void OnPosition(IntVector2 value)
    {
        transform.localPosition = new Vector3(value.x, value.y);
    }

    protected void destroy() {
        gameObject.Unlink();
        Destroy(gameObject);
    }
}

public interface IPositionListener:IGamePositionListener,IAudioPositionListener{}
public interface IDestroyedListener:IGameDestroyedListener,IAudioDestroyedListener{}
