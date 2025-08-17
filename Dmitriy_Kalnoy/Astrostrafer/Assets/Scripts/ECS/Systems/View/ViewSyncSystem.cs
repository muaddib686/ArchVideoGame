using Entitas;
using UnityEngine;

namespace Astrostrafer.ECS
{
    public sealed class ViewSyncSystem : IExecuteSystem
    {
        private readonly GameContext _game;

        public ViewSyncSystem(Contexts contexts)
        {
            _game = contexts.game;
        }

        public void Execute()
        {
            var entities = _game.GetGroup(GameMatcher.AllOf(GameMatcher.AstrostraferECSView, GameMatcher.AstrostraferECSPosition)).GetEntities();
            foreach (var e in entities)
            {
                var t = e.astrostraferECSView.value;
                var p = e.astrostraferECSPosition.value;
                if (t != null)
                {
                    t.position = new Vector3(p.x, p.y, t.position.z);
                }
            }
        }
    }
}


