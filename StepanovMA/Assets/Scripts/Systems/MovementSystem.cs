using Entitas;
using UnityEngine;

public class MovementSystem : IExecuteSystem {
    private readonly Contexts contexts;

    public MovementSystem(Contexts contexts) {
        this.contexts = contexts;
    }

    public void Execute() {
        // Get game state entity
        var gameStateEntities = contexts.game.GetGroup(GameMatcher.GameState).GetEntities();
        if (gameStateEntities.Length == 0) return;
        var gameStateEntity = gameStateEntities[0];

        var gameState = gameStateEntity.gameState;
        if (gameState.currentState != GameStateComponent.State.Playing) return;

        var entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.Velocity)).GetEntities();

        foreach (var entity in entities) {
            var position = entity.position;
            var velocity = entity.velocity;

            // Update position based on velocity
            entity.ReplacePosition(position.value + velocity.value * Time.deltaTime);
        }
    }
}