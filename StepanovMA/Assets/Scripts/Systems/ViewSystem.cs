using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class ViewSystem : IExecuteSystem, ICleanupSystem {
    private readonly Contexts contexts;
    private readonly Dictionary<GameEntity, GameObject> entityViews;

    public ViewSystem(Contexts contexts) {
        this.contexts = contexts;
        this.entityViews = new Dictionary<GameEntity, GameObject>();
    }

    public void Execute() {
        // Get game state entity
        var gameStateEntities = contexts.game.GetGroup(GameMatcher.GameState).GetEntities();
        if (gameStateEntities.Length == 0) return;
        var gameStateEntity = gameStateEntities[0];

        var gameState = gameStateEntity.gameState;
        if (gameState.currentState == GameStateComponent.State.GameOver) {
            Cleanup();
            return;
        }

        // Create views for new entities
        var entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.Size, GameMatcher.Sprite)).GetEntities();

        foreach (var entity in entities) {
            if (!entityViews.ContainsKey(entity)) {
                CreateView(entity);
            }
        }

        // Update positions of existing views
        foreach (var kvp in entityViews) {
            var entity = kvp.Key;
            var view = kvp.Value;

            if (entity.hasPosition && view != null) {
                view.transform.position = new Vector3(entity.position.value.x, entity.position.value.y, 0);
            }
        }
    }

    public void Cleanup() {
        // Remove all game objects
        foreach (var kvp in entityViews) {
            var view = kvp.Value;
            if (view != null) {
                Object.Destroy(view);
            }
        }
        entityViews.Clear();
    }

    private void CreateView(GameEntity entity) {
        if (!entity.hasPosition || !entity.hasSize || !entity.hasSprite)
            return;

        var spriteName = entity.sprite.spriteName;
        var position = entity.position.value;
        var size = entity.size.value;

        // Create GameObject
        var view = new GameObject($"{spriteName}_{entity.creationIndex}");
        view.transform.position = new Vector3(position.x, position.y, 0);
        view.transform.localScale = new Vector3(size.x, size.y, 1);

        var sprite = Resources.Load<Sprite>(spriteName);
        if (sprite == null) {
            Debug.LogError($"Sprite {spriteName} not found");
            return;
        }

        var spriteRenderer = view.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        entityViews[entity] = view;
    }
}