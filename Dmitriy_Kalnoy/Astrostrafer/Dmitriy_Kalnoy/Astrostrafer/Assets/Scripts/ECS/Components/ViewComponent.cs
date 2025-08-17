using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Astrostrafer.ECS
{
    [Game]
    public sealed class ViewComponent : IComponent
    {
        public Transform value;
    }
}


