using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Astrostrafer.ECS
{
    public sealed class CleanupWorldOnEnterMenuSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext _game;

        public CleanupWorldOnEnterMenuSystem(Contexts contexts) : base(contexts.game)
        {
            _game = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AstrostraferECSMainMenuState.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isAstrostraferECSMainMenuState;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            var all = _game.GetEntities();
            foreach (var e in all)
            {
                // сохраняем важные уникальные сущности
                if (e.hasAstrostraferECSGameConfig ||
                    e.isAstrostraferECSMainMenuState ||
                    e.isAstrostraferECSGamePlayState ||
                    e.isAstrostraferECSPauseState)
                {
                    continue;
                }

                // удаляем игровую динамику (игрок, астероиды, и т.п.)
                if (e.hasAstrostraferECSView && e.astrostraferECSView.value != null)
                {
                    Object.Destroy(e.astrostraferECSView.value.gameObject);
                }
                e.Destroy();
            }
        }
    }
}


