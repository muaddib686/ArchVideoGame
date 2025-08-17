using Entitas;

namespace Astrostrafer.ECS
{
    public sealed class ReturnToMenuOnGameOverSystem : IExecuteSystem
    {
        private readonly GameContext _game;

        public ReturnToMenuOnGameOverSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Execute()
        {
            if (!_game.isAstrostraferECSGamePlayState) return;
            if (_game.isAstrostraferECSGameOver)
            {
                _game.isAstrostraferECSGamePlayState = false;
                _game.isAstrostraferECSMainMenuState = true;
                // очистим флаг, чтобы не зациклиться
                _game.isAstrostraferECSGameOver = false;
            }
        }
    }
}


