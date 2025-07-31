using Entitas;
using UnityEngine;

public class CollisionDetectionSystem : IExecuteSystem {
    private readonly Contexts contexts;

    public CollisionDetectionSystem(Contexts contexts) {
        this.contexts = contexts;
    }

    public void Execute() {
        // Get player entity
        var playerEntities = contexts.game.GetGroup(GameMatcher.PlayerTag).GetEntities();
        if (playerEntities.Length == 0) return;
        var playerEntity = playerEntities[0];

        // Get game state entity
        var gameStateEntities = contexts.game.GetGroup(GameMatcher.GameState).GetEntities();
        if (gameStateEntities.Length == 0) return;
        var gameStateEntity = gameStateEntities[0];
        var gameState = gameStateEntity.gameState;
        if (gameState.currentState != GameStateComponent.State.Playing) return;

        var playerPosition = playerEntity.position;
        var playerSize = playerEntity.size;

        // Check collision with asteroids
        var asteroidEntities = contexts.game.GetGroup(GameMatcher.AsteroidTag).GetEntities();
        foreach (var entity in asteroidEntities) {
            var asteroidPosition = entity.position;
            var asteroidSize = entity.size;

            if (CheckCollision(playerPosition.value, playerSize.value,
                             asteroidPosition.value, asteroidSize.value)) {
                // Collision detected - transition to game over
                gameStateEntity.ReplaceGameState(GameStateComponent.State.GameOver);
                Debug.Log("Game Over!");
                return;
            }
        }
    }

    private bool CheckCollision(Vector2 pos1, Vector2 size1, Vector2 pos2, Vector2 size2) {
        // Simple AABB collision detection
        float halfWidth1 = size1.x * 0.5f;
        float halfHeight1 = size1.y * 0.5f;
        float halfWidth2 = size2.x * 0.5f;
        float halfHeight2 = size2.y * 0.5f;

        return pos1.x - halfWidth1 < pos2.x + halfWidth2 &&
               pos1.x + halfWidth1 > pos2.x - halfWidth2 &&
               pos1.y - halfHeight1 < pos2.y + halfHeight2 &&
               pos1.y + halfHeight1 > pos2.y - halfHeight2;
    }
}
