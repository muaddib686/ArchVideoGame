using Entitas;
using UnityEngine;

namespace Astrostrafer.ECS
{
    public sealed class SetupGameConfigSystem : IInitializeSystem
    {
        private readonly GameContext _game;

        public SetupGameConfigSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Initialize()
        {
            var cam = Camera.main;
            float halfHeight = cam.orthographicSize;
            float halfWidth = halfHeight * cam.aspect;
            var c = cam.transform.position;

            _game.SetAstrostraferECSGameConfig(
                c.x - halfWidth,
                c.x + halfWidth,
                c.y + halfHeight,
                c.y - halfHeight,
                GameConstants.PlayerSpeed,
                GameConstants.PlayerRadius,
                GameConstants.AsteroidMinSpawnInterval,
                GameConstants.AsteroidMaxSpawnInterval,
                GameConstants.AsteroidMinSpeed,
                GameConstants.AsteroidMaxSpeed,
                GameConstants.AsteroidRadius,
                2f
            );
        }
    }
}


