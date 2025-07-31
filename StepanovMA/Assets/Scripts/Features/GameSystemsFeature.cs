using Entitas;

public sealed class GameSystems : Feature {
    public GameSystems(Contexts contexts) {
        // Input systems
        Add(new InputSystem(contexts));

        // UI systems
        Add(new UISystem(contexts));

        // Game state management system (centralized)
        Add(new GameStateManagementSystem(contexts));

        // Game logic systems
        Add(new PlayerMovementSystem(contexts));
        Add(new AsteroidSpawnerSystem(contexts));
        Add(new MovementSystem(contexts));

        // Collision and cleanup systems
        Add(new CollisionDetectionSystem(contexts));
        Add(new OutOfBoundsSystem(contexts));

        // View systems
        Add(new ViewSystem(contexts));
    }
}
