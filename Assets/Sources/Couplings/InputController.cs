using UnityEngine;
using Entitas.Unity;
using Entitas;

public class InputController : MonoBehaviour {

    Contexts _contexts;
    View _view;
    GameEntity entity;

    void Awake() {
        _contexts = Contexts.sharedInstance;
    }

	private void Start()
	{
        _view = GetComponent<View>();
        entity = (GameEntity)_view.gameObject.GetEntityLink().entity;
	}

	private void OnMouseUpAsButton()
    {
        var pos = transform.position;
        _contexts.input.CreateEntity()
                 .AddMouseUp(entity);
    }
}
