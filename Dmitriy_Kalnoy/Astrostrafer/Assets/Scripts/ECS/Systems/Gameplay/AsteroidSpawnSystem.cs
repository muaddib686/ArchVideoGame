using Entitas;
using UnityEngine;

namespace Astrostrafer.ECS
{
    public sealed class AsteroidSpawnSystem : IInitializeSystem, IExecuteSystem
    {
        private readonly GameContext _game;
        private float _timer;
        private float _nextInterval;

        public AsteroidSpawnSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Initialize()
        {
            ScheduleNext();
        }

        public void Execute()
        {
            if (!_game.isAstrostraferECSGamePlayState) return;

            var cfg = _game.astrostraferECSGameConfig;
            _timer += Time.deltaTime;
            if (_timer >= _nextInterval)
            {
                _timer = 0f;
                ScheduleNext();
                float x = Random.Range(cfg.left + 0.5f, cfg.right - 0.5f);
                float y = cfg.top + 1.0f;
                var e = _game.CreateEntity();
                e.isAstrostraferECSAsteroidTag = true;
                e.AddAstrostraferECSPosition(new Vector2(x, y));
                float speed = Random.Range(cfg.asteroidMinSpeed, cfg.asteroidMaxSpeed);
                e.AddAstrostraferECSVelocity(new Vector2(0f, -speed));
                e.AddAstrostraferECSRadius(cfg.asteroidRadius);
            }
        }

        private void ScheduleNext()
        {
            var cfg = _game.astrostraferECSGameConfig;
            _nextInterval = Random.Range(cfg.asteroidMinSpawnInterval, cfg.asteroidMaxSpawnInterval);
        }
    }
}


