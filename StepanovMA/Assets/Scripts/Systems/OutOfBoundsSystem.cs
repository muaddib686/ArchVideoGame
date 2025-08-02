using Entitas;

public class OutOfBoundsSystem : IExecuteSystem {
    private readonly Contexts contexts;

    public OutOfBoundsSystem(Contexts contexts) {
        this.contexts = contexts;
    }

    public void Execute() {
        float halfWidth = Config.ScreenWidth * 0.5f;
        float halfHeight = Config.ScreenHeight * 0.5f;

        var entities = contexts.game.GetGroup(GameMatcher.Position).GetEntities();
        var entitiesToDestroy = new System.Collections.Generic.List<GameEntity>();

        foreach (var entity in entities) {
            var position = entity.position;

            // Check if entity is out of bounds.
            // Add some tolerance, so entities fly away from the screen before
            // disappearing.
            if (position.value.x < -halfWidth * Config.OutOfBoundsTolerance ||
                position.value.x > halfWidth * Config.OutOfBoundsTolerance ||
                position.value.y < -halfHeight * Config.OutOfBoundsTolerance ||
                position.value.y > halfHeight * Config.OutOfBoundsTolerance) {
                // Don't destroy player entity
                if (!entity.isPlayerTag) {
                    entitiesToDestroy.Add(entity);
                }
            }
        }

        // Destroy out of bounds entities
        foreach (var entity in entitiesToDestroy) {
            entity.Destroy();
        }
    }
}
