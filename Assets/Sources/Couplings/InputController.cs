using UnityEngine;

public class InputController : MonoBehaviour {

    Contexts _contexts;

    void Awake() {
        _contexts = Contexts.sharedInstance;
    }

    private void OnMouseUpAsButton()
    {
        var pos = transform.position;
        _contexts.input.CreateEntity()
            .AddInput((int)pos.x, (int)pos.y);
    }
}
