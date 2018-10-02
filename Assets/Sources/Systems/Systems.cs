public sealed class Systems : Feature {

    public Systems(Contexts contexts) {

        // Audio
        Add(new AudioSystems(contexts));

        // Input
        Add(new InputSystems(contexts));

        // Create ui objects
        Add(new GameSystems(contexts));

        // Events
        Add(new AudioEventSystems(contexts));
        //Add(new InputEventSystems(contexts));
        Add(new GameEventSystems(contexts));

        // Cleanup
        Add(new DestroyEntitySystem(contexts));
    }
}
