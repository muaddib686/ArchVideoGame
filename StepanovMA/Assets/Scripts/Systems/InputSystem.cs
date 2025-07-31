using Entitas;
using UnityEngine;

/* System that handles the input from the player. */
public class InputSystem : IExecuteSystem {
    private readonly Contexts contexts;

    public InputSystem(Contexts contexts) {
        this.contexts = contexts;
    }

    public void Execute() {
        // Get game state entity
        var gameStateEntities = contexts.game.GetGroup(GameMatcher.GameState).GetEntities();
        if (gameStateEntities.Length == 0) return;
        var gameStateEntity = gameStateEntities[0];

        var gameState = gameStateEntity.gameState;
        if (gameState.currentState != GameStateComponent.State.Playing) return;

        float horizontalInput = 0f;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            horizontalInput -= 1f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            horizontalInput += 1f;
        }

        // Find or create input entity
        var inputEntities = contexts.game.GetGroup(GameMatcher.Input).GetEntities();
        if (inputEntities.Length == 0) {
            GameEntity inputEntity = contexts.game.CreateEntity();
            inputEntity.AddInput(horizontalInput);
        }
        else {
            GameEntity inputEntity = inputEntities[0];
            inputEntity.ReplaceInput(horizontalInput);
        }
    }
}
