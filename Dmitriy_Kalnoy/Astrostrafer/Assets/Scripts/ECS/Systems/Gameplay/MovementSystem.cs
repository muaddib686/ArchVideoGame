using Entitas;
using UnityEngine;

namespace Astrostrafer.ECS
{
    public sealed class MovementSystem : IExecuteSystem
    {
        private readonly GameContext _game;

        public MovementSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Execute()
        {
            if (!_game.isAstrostraferECSGamePlayState) return;

            float dt = Time.deltaTime;
            var movers = _game.GetGroup(GameMatcher.AllOf(GameMatcher.AstrostraferECSPosition, GameMatcher.AstrostraferECSVelocity)).GetEntities();
            foreach (var e in movers)
            {
                var p = e.astrostraferECSPosition.value + e.astrostraferECSVelocity.value * dt;
                e.ReplaceAstrostraferECSPosition(p);
            }
        }
    }
}


