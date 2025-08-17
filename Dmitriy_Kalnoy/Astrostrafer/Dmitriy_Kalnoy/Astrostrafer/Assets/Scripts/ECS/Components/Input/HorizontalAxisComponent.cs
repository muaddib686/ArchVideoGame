using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Astrostrafer.ECS.Input
{
    [Input]
    public sealed class HorizontalAxisComponent : IComponent
    {
        public float value;
    }
}


