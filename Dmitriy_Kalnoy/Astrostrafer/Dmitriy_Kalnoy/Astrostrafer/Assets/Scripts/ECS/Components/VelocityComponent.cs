using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Astrostrafer.ECS
{
    [Game]
    public sealed class VelocityComponent : IComponent
    {
        public Vector2 value;
    }
}


