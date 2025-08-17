using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Astrostrafer.ECS
{
    public sealed class EnterGamePlayCreatePlayerSystem : ReactiveSystem<GameEntity>, IInitializeSystem
    {
        private readonly GameContext _game;

        public EnterGamePlayCreatePlayerSystem(Contexts contexts) : base(contexts.game)
        {
            _game = contexts.game;
        }

        public void Initialize()
        {
            // Если игра стартует сразу в GamePlay (на всякий), создадим игрока
            if (_game.isAstrostraferECSGamePlayState)
            {
                CreatePlayer();
            }
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AstrostraferECSGamePlayState.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isAstrostraferECSGamePlayState;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            // создаём игрока, только если его ещё нет
            var existing = _game.GetGroup(GameMatcher.AstrostraferECSPlayerTag).GetEntities();
            if (existing.Length == 0)
            {
                CreatePlayer();
            }
        }

        private void CreatePlayer()
        {
            var cfg = _game.astrostraferECSGameConfig;
            var player = _game.CreateEntity();
            player.isAstrostraferECSPlayerTag = true;
            player.AddAstrostraferECSPosition(new Vector2(0f, cfg.bottom + 1.5f));
            player.AddAstrostraferECSVelocity(Vector2.zero);
            player.AddAstrostraferECSRadius(cfg.playerRadius);
            Debug.Log("Player entity created at y=" + (cfg.bottom + 1.5f));
        }
    }
}


