using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using System.Linq;


public class GameSystems : Feature
{
    public GameSystems(Contexts contexts) : base("Game Systems")
    {
        // order is respected 
        //Add(new CreateTestObjectsSystem(contexts));
        Add(new AssignButtonParents(contexts));
        Add(new UpdateButtonListeners(contexts));
        Add(new UpdatePatternBindings(contexts));
        Add(new UpdateStepPulse(contexts));
    }
}

public sealed class AssignButtonParents : ReactiveSystem<GameEntity>
{

    public AssignButtonParents(Contexts contexts):base(contexts.game)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            e.holder.items.ForEach((GameEntity obj) => obj.AddParent(e));
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasPatternBinding && entity.holder.items.Count>0;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Holder); 
    }
}

public sealed class CreateTestObjectsSystem : IInitializeSystem
{
    public EntityService entityService = EntityService.singleton;
    //readonly GameContext _context;

    public CreateTestObjectsSystem(Contexts contexts)
    {
        //_context = contexts.game;
    }

    public void Initialize()
    {
        //entityService.CreateButtonLayout(8);
    }
}

public sealed class UpdateButtonListeners : ReactiveSystem<GameEntity>
{
    
    public UpdateButtonListeners (Contexts contexts):base(contexts.game){
        
    }
    protected override void Execute(List<GameEntity> entities)
    {
        foreach(var e in entities){
            e.buttonListener.listener.ButtonChanged(e.button.state);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasButtonListener;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Button);
    }
}

public sealed class UpdatePatternBindings : ReactiveSystem<GameEntity>{

    public UpdatePatternBindings(Contexts contexts) : base(contexts.game)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            var state = e.button.state;
            bool active = (state == ButtonState.Off) ? false : true;
            e.parent.entity.patternBinding.entity.pattern.steps[e.itemIndex.value].ReplaceStep(active);
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasItemIndex;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Button);
    }
}

/// <summary>
/// Update step pulse. Link from audio context to game.
/// </summary>
public sealed class UpdateStepPulse : ReactiveSystem<GameEntity>
{

    //IGroup<GameEntity> group;

    public UpdateStepPulse(Contexts contexts) : base(contexts.game)
    {
        //group = contexts.game.GetGroup(GameMatcher.PatternBinding);
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in entities)
        {
            for (int i = 0; i < e.holder.items.Count;i++){
                if (i == e.patternPulse.step)
                    e.holder.items[e.patternPulse.step].isStepAction = true;
                else if (e.holder.items[i].isStepAction)
                    e.holder.items[i].isStepAction = false;
            }
        }
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasHolder && entity.holder.items.Count>0;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.PatternPulse);
    }
}

