using System.Collections.Generic;
using Entitas;
using UnityEngine;
using Astrostrafer.View;

namespace Astrostrafer.ECS
{
    public sealed class ViewCreationSystem : ReactiveSystem<GameEntity>
    {
        public ViewCreationSystem(Contexts contexts) : base(contexts.game) { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.AstrostraferECSPlayerTag, GameMatcher.AstrostraferECSAsteroidTag).Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return (entity.isAstrostraferECSPlayerTag || entity.isAstrostraferECSAsteroidTag) && !entity.hasAstrostraferECSView && entity.hasAstrostraferECSPosition;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var e in entities)
            {
                var prefab = e.isAstrostraferECSPlayerTag ? ViewServices.PlayerPrefab : ViewServices.AsteroidPrefab;
                if (prefab == null) continue;
                var go = Object.Instantiate(prefab, ViewServices.ViewRoot);
                var t = go.transform;
                var p = e.astrostraferECSPosition.value;
                t.position = new Vector3(p.x, p.y, 0f);
                e.AddAstrostraferECSView(t);
                Debug.Log($"View created for {(e.isAstrostraferECSPlayerTag ? "Player" : "Asteroid")} at {t.position}");
            }
        }
    }
}


