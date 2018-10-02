using System;
using System.Collections.Generic;

public class EntityService {

    public RandomService randomService = RandomService.game;

    public static EntityService singleton = new EntityService();

    static readonly string[] _pieces = {
        Res.Button
    };

    Contexts _contexts;

    public void Initialize(Contexts contexts) {
        _contexts = contexts;
    }

    internal void CreateButtonLayout(int y, int numButtons, AudioEntity pattern){
        List<GameEntity> buttons = new List<GameEntity>();
        for (int i = 0; i < numButtons; i++)
        {
            buttons.Add(CreateButton(i,y));
        }

        var e = _contexts.game.CreateEntity();
        e.AddHolder(buttons, HolderArrangement.Linear);

        e.AddPatternBinding(pattern);
        pattern.AddAttachedView(e);
    }

    internal GameEntity CreateButton(int i, int y)
	{
        var e = _contexts.game.CreateEntity();
        e.AddPosition(new IntVector2(i*2, y));
        e.isInteractive = true;
        e.AddAsset(_pieces[0]);
        e.AddButton(0);
        e.AddItemIndex(i);
        return e;
	}
}
