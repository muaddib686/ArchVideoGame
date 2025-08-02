using Entitas;
using UnityEngine;

public class AsteroidSpawnerSystem : IExecuteSystem {
    private readonly Contexts contexts;
    private float spawnTimer = 0f;

    public AsteroidSpawnerSystem(Contexts contexts) {
        this.contexts = contexts;
    }

    public void Execute() {
        // Get game state entity
        var gameStateEntities = contexts.game.GetGroup(GameMatcher.GameState).GetEntities();
        if (gameStateEntities.Length == 0) return;
        var gameStateEntity = gameStateEntities[0];

        var gameState = gameStateEntity.gameState;
        if (gameState.currentState != GameStateComponent.State.Playing) return;

        // Update spawn timer
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= Config.AsteroidSpawnRate) {
            spawnTimer = 0f;
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid() {
        // Create asteroid entity
        var asteroid = contexts.game.CreateEntity();
        asteroid.isAsteroidTag = true;

        // Set position at top of screen with random X
        float randomX = Random.Range(-Config.ScreenWidth * 0.5f, Config.ScreenWidth * 0.5f);
        asteroid.AddPosition(new Vector2(randomX, Config.ScreenHeight * 0.5f));

        // Set velocity with random angle
        float angleInDegrees = Random.Range(-Config.AsteroidAngleRange, Config.AsteroidAngleRange);
        float angleInRadians = angleInDegrees * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Sin(angleInRadians), -Mathf.Cos(angleInRadians));
        asteroid.AddVelocity(direction * Config.AsteroidVelocity);

        // Set size
        asteroid.AddSize(Config.AsteroidSize);

        // Set sprite
        asteroid.AddSprite(Config.AsteroidSpritePath);
    }
}
