using Entitas;

namespace Astrostrafer.ECS
{
    public sealed class OffscreenDespawnSystem : IExecuteSystem
    {
        private readonly GameContext _game;

        public OffscreenDespawnSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Execute()
        {
            if (!_game.isAstrostraferECSGamePlayState) return;

            var cfg = _game.astrostraferECSGameConfig;
            float bottom = cfg.bottom - cfg.despawnMargin;
            var asteroids = _game.GetGroup(GameMatcher.AllOf(GameMatcher.AstrostraferECSAsteroidTag, GameMatcher.AstrostraferECSPosition)).GetEntities();
            foreach (var e in asteroids)
            {
                if (e.astrostraferECSPosition.value.y < bottom)
                {
                    e.Destroy();
                }
            }
        }
    }
}


