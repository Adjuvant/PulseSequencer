using System;

public class Services {

    public static Services singleton = new Services();

    public void Initialize(Contexts contexts, GameController gameController) {
        var random = new Random(DateTime.UtcNow.Millisecond);
        UnityEngine.Random.InitState(random.Next());
        RandomService.game.Initialize(random.Next());
        RandomService.view.Initialize(random.Next());
        EntityService.singleton.Initialize(contexts);
        AudioService.singleton.Initialize(contexts);
        ViewService.singleton.Initialize(contexts);
    }
}
