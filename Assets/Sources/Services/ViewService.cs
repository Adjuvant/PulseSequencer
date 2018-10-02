using UnityEngine;


public interface IAssetListener : IAudioAssetListener, IGameAssetListener { }

public class ViewService : IAssetListener {
    readonly Transform _viewContainer = new GameObject("Views").transform;
 
    public static ViewService singleton = new ViewService();

    Contexts _contexts;

    public void Initialize(Contexts contexts) {
        _contexts = contexts;

        var eg = contexts.game.CreateEntity();
        eg.AddGameAssetListener(this);
        var ea = contexts.audio.CreateEntity();
        ea.AddAudioAssetListener(this);
    }

    public void OnAsset(IViewable entity, string value)
    {
        var prefab = Resources.Load<GameObject>(value);
        var obj = Object.Instantiate(prefab, _viewContainer);

        var c = entity.contextInfo.name;
        obj.name = c + " " + value;

        switch(c)
        {
            case "Audio":
            {
                var e = (AudioEntity)entity;
                e.AddAudioPositionListener(obj.GetComponent<IAudioPositionListener>());
                e.AddAudioDestroyedListener(obj.GetComponent<IAudioDestroyedListener>());
                break;
            }
            case "Game":
            {
                var e = (GameEntity)entity;
                e.AddGamePositionListener(obj.GetComponent<IGamePositionListener>());
                e.AddGameDestroyedListener(obj.GetComponent<IGameDestroyedListener>());
                break;
            }
        }
        // Currently the responsiblity of MB objects to add their listeners
        var iview = obj.GetComponent<IView>();
        iview.Link(entity, _contexts.game);
    }

    public void OnAsset(GameEntity entity, string value) {
        OnAsset((IViewable)entity, value);
    }

    public void OnAsset(AudioEntity entity, string value)
    {
        OnAsset((IViewable)entity, value);
    }
}

