using Entitas;

namespace Astrostrafer.ECS
{
    public sealed class TogglePauseOnPausePressedSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly InputContext _input;

        public TogglePauseOnPausePressedSystem(Contexts contexts)
        {
            _game = contexts.game;
            _input = contexts.input;
        }

        public void Execute()
        {
            // Если нет нажатия — выходим
            if (_input.GetGroup(InputMatcher.AstrostraferECSInputPausePressed).count == 0) return;

            // Переключатель: из GamePlay → Pause, из Pause → GamePlay
            if (_game.isAstrostraferECSGamePlayState)
            {
                _game.isAstrostraferECSGamePlayState = false;
                _game.isAstrostraferECSPauseState = true;
            }
            else if (_game.isAstrostraferECSPauseState)
            {
                _game.isAstrostraferECSPauseState = false;
                _game.isAstrostraferECSGamePlayState = true;
            }
        }
    }
}


