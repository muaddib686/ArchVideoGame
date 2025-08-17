using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Astrostrafer.ECS
{
    [Game]
    public sealed class PositionComponent : IComponent
    {
        public Vector2 value;
    }
}


