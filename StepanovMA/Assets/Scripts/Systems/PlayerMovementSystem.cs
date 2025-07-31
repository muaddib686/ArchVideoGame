using Entitas;
using UnityEngine;

public class PlayerMovementSystem : IExecuteSystem {
    private readonly Contexts contexts;

    public PlayerMovementSystem(Contexts contexts) {
        this.contexts = contexts;
    }

    public void Execute() {
        // Get game state entity
        var gameStateEntities = contexts.game.GetGroup(GameMatcher.GameState).GetEntities();
        if (gameStateEntities.Length == 0) return;
        var gameStateEntity = gameStateEntities[0];

        var gameState = gameStateEntity.gameState;
        if (gameState.currentState != GameStateComponent.State.Playing) return;

        // Get or create player entity
        var playerEntities = contexts.game.GetGroup(GameMatcher.PlayerTag).GetEntities();
        GameEntity playerEntity;
        if (playerEntities.Length == 0) {
            playerEntity = contexts.game.CreateEntity();
            playerEntity.isPlayerTag = true;
            playerEntity.AddPosition(Config.PlayerStartPosition);
            playerEntity.AddVelocity(Vector2.zero);
            playerEntity.AddSize(Config.PlayerSize);
            playerEntity.AddSprite(Config.PlayerSpritePath);
        }
        else {
            playerEntity = playerEntities[0];
        }

        // Get input
        var inputEntities = contexts.game.GetGroup(GameMatcher.Input).GetEntities();
        if (inputEntities.Length == 0) return;
        var inputEntity = inputEntities[0];

        var inputComponent = inputEntity.input;
        var positionComponent = playerEntity.position;
        var velocityComponent = playerEntity.velocity;

        // Calculate movement
        float moveSpeed = Config.PlayerSpeed;
        float deltaTime = Time.deltaTime;
        float movement = inputComponent.horizontalInput * moveSpeed * deltaTime;

        // Update position
        Vector2 newPosition = positionComponent.value;
        newPosition.x += movement;

        // Clamp to screen bounds
        float halfWidth = Config.ScreenWidth * 0.5f;
        newPosition.x = Mathf.Clamp(newPosition.x, -halfWidth, halfWidth);

        playerEntity.ReplacePosition(newPosition);

        // Update velocity for other systems
        playerEntity.ReplaceVelocity(new Vector2(inputComponent.horizontalInput * moveSpeed, 0));
    }
}
