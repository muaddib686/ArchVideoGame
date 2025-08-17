using Entitas;
using UnityEngine;

namespace Astrostrafer.ECS
{
    public sealed class ApplyPauseTimeScaleSystem : IExecuteSystem
    {
        private readonly GameContext _game;

        public ApplyPauseTimeScaleSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Execute()
        {
            Time.timeScale = _game.isAstrostraferECSPauseState ? 0f : 1f;
        }
    }
}


