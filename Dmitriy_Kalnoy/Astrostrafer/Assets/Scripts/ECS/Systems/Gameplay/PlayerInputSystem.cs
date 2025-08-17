using Entitas;
using UnityEngine;

namespace Astrostrafer.ECS
{
    public sealed class PlayerInputSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly InputContext _input;

        public PlayerInputSystem(Contexts contexts)
        {
            _game = contexts.game;
            _input = contexts.input;
        }

        public void Execute()
        {
            if (!_game.isAstrostraferECSGamePlayState) return;

            float axis = 0f;
            var axisEntity = _input.GetGroup(InputMatcher.AstrostraferECSInputHorizontalAxis).GetSingleEntity();
            if (axisEntity != null && axisEntity.hasAstrostraferECSInputHorizontalAxis)
            {
                axis = axisEntity.astrostraferECSInputHorizontalAxis.value;
            }

            var cfg = _game.astrostraferECSGameConfig;
            var players = _game.GetGroup(GameMatcher.AllOf(GameMatcher.AstrostraferECSPlayerTag, GameMatcher.AstrostraferECSVelocity)).GetEntities();
            foreach (var p in players)
            {
                p.ReplaceAstrostraferECSVelocity(new Vector2(axis * cfg.playerSpeed, 0f));
            }
        }
    }
}


