using Entitas;
using UnityEngine;

/* Centralized system for managing game state transitions and entity lifecycle.
 * This system consolidates all game state logic that was previously scattered
 * across UIButtonProcessingSystem, RestartSystem, and CollisionDetectionSystem.
 */
public class GameStateManagementSystem : IExecuteSystem, IInitializeSystem {
    private readonly Contexts contexts;
    private readonly IGroup<GameEntity> gameStateGroup;
    private readonly IGroup<GameEntity> uiButtonGroup;

    public GameStateManagementSystem(Contexts contexts) {
        this.contexts = contexts;
        this.gameStateGroup = contexts.game.GetGroup(GameMatcher.GameState);
        this.uiButtonGroup = contexts.game.GetGroup(GameMatcher.UIButton);
    }

    public void Initialize() {
        // Get or create game state entity
        var gameStateEntities = gameStateGroup.GetEntities();
        if (gameStateEntities.Length == 0) {
            var gameStateEntity = contexts.game.CreateEntity();
            gameStateEntity.AddGameState(GameStateComponent.State.MainMenu);
        }
    }

    public void Execute() {
        var gameStateEntities = gameStateGroup.GetEntities();
        if (gameStateEntities.Length == 0) return;

        // Handle UI button events
        ProcessUIButtonEvents();

        // Handle keyboard restart input
        ProcessKeyboardInput();
    }

    private void ProcessUIButtonEvents() {
        var uiButtonEntities = uiButtonGroup.GetEntities();

        foreach (var entity in uiButtonEntities) {
            var buttonType = entity.uIButton.buttonType;

            // If entity with UIButton component is found,
            // the button was clicked in the UI. Process it.
            switch (buttonType) {
                case UIButtonComponent.ButtonType.Play:
                    TransitionToPlaying();
                    break;
                case UIButtonComponent.ButtonType.Restart:
                    RestartGame();
                    break;
            }
        }

        // Delete UI button entities after processing
        foreach (var entity in uiButtonEntities) {
            entity.Destroy();
        }
    }

    private void ProcessKeyboardInput() {
        if (Input.GetKeyDown(KeyCode.R)) {
            RestartGame();
        }
    }

    public void TransitionToPlaying() {
        var gameStateEntities = gameStateGroup.GetEntities();
        if (gameStateEntities.Length == 0) return;
        var gameStateEntity = gameStateEntities[0];
        var currentState = gameStateEntity.gameState.currentState;

        // Allow transition to playing from main menu or game over
        if (currentState == GameStateComponent.State.Playing) {
            return;
        }

        // Clear game entities and reset to playing state
        ClearGameEntities();
        gameStateEntity.ReplaceGameState(GameStateComponent.State.Playing);

        Debug.Log("Game started!");
    }

    public void RestartGame() {
        // Clear all entities except config and game state
        ClearGameEntities();

        var gameStateEntities = gameStateGroup.GetEntities();
        if (gameStateEntities.Length > 0) {
            gameStateEntities[0].ReplaceGameState(GameStateComponent.State.Playing);
        }

        Debug.Log("Game restarted!");
    }

    private void ClearGameEntities() {
        // Get all entities that should be destroyed (player and asteroids)
        var entities = contexts.game
            .GetGroup(GameMatcher.AnyOf(GameMatcher.AsteroidTag, GameMatcher.PlayerTag))
            .GetEntities();

        foreach (var entity in entities) {
            entity.Destroy();
        }
    }
}