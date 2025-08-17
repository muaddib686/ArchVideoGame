using Entitas;

namespace Astrostrafer.ECS
{
    public sealed class SetInitialStateSystem : IInitializeSystem
    {
        private readonly GameContext _game;

        public SetInitialStateSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Initialize()
        {
            _game.isAstrostraferECSMainMenuState = true;
        }
    }
}


