using Entitas;
using UnityEngine;

namespace Astrostrafer.ECS
{
    public sealed class CollisionSystem : IExecuteSystem
    {
        private readonly GameContext _game;

        public CollisionSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Execute()
        {
            if (!_game.isAstrostraferECSGamePlayState) return;

            var players = _game.GetGroup(GameMatcher.AllOf(GameMatcher.AstrostraferECSPlayerTag, GameMatcher.AstrostraferECSPosition, GameMatcher.AstrostraferECSRadius)).GetEntities();
            var asteroids = _game.GetGroup(GameMatcher.AllOf(GameMatcher.AstrostraferECSAsteroidTag, GameMatcher.AstrostraferECSPosition, GameMatcher.AstrostraferECSRadius)).GetEntities();

            foreach (var p in players)
            {
                foreach (var a in asteroids)
                {
                    if (Vector2.Distance(p.astrostraferECSPosition.value, a.astrostraferECSPosition.value) <= p.astrostraferECSRadius.value + a.astrostraferECSRadius.value)
                    {
                        _game.isAstrostraferECSGameOver = true;
                        return;
                    }
                }
            }
        }
    }
}


