using Entitas;
using UnityEngine;

namespace Astrostrafer.ECS
{
    public sealed class BoundsClampSystem : IExecuteSystem
    {
        private readonly GameContext _game;

        public BoundsClampSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Execute()
        {
            if (!_game.isAstrostraferECSGamePlayState) return;

            var cfg = _game.astrostraferECSGameConfig;
            var players = _game.GetGroup(GameMatcher.AllOf(GameMatcher.AstrostraferECSPlayerTag, GameMatcher.AstrostraferECSPosition, GameMatcher.AstrostraferECSRadius)).GetEntities();
            foreach (var e in players)
            {
                var pos = e.astrostraferECSPosition.value;
                float r = e.astrostraferECSRadius.value;
                pos.x = Mathf.Clamp(pos.x, cfg.left + r, cfg.right - r);
                e.ReplaceAstrostraferECSPosition(pos);
            }
        }
    }
}


