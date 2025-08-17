using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Astrostrafer.ECS
{
    [Game, Unique]
    public sealed class GameConfigComponent : IComponent
    {
        public float left;
        public float right;
        public float top;
        public float bottom;

        public float playerSpeed;
        public float playerRadius;

        public float asteroidMinSpawnInterval;
        public float asteroidMaxSpawnInterval;
        public float asteroidMinSpeed;
        public float asteroidMaxSpeed;
        public float asteroidRadius;

        public float despawnMargin;
    }
}


