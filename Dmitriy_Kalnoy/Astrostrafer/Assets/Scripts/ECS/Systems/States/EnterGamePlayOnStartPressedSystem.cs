using Entitas;

namespace Astrostrafer.ECS
{
    public sealed class EnterGamePlayOnStartPressedSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly InputContext _input;

        public EnterGamePlayOnStartPressedSystem(Contexts contexts)
        {
            _game = contexts.game;
            _input = contexts.input;
        }

        public void Execute()
        {
            if (!_game.isAstrostraferECSMainMenuState) return;
            var pressed = _input.GetGroup(InputMatcher.AstrostraferECSInputStartPressed).count > 0;
            if (pressed)
            {
                _game.isAstrostraferECSMainMenuState = false;
                _game.isAstrostraferECSGamePlayState = true;
                UnityEngine.Debug.Log("Enter GamePlay");
            }
        }
    }
}


