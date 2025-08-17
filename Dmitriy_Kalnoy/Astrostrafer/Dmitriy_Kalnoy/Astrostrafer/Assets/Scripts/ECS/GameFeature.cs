using Entitas;

namespace Astrostrafer.ECS
{
    public sealed class GameFeature : Feature
    {
        public GameFeature(Contexts contexts) : base("GameFeature")
        {
            // Init
            Add(new SetInitialStateSystem(contexts));
            Add(new SetupGameConfigSystem(contexts));

            // State transitions
            Add(new EnterGamePlayOnStartPressedSystem(contexts));
            Add(new TogglePauseOnPausePressedSystem(contexts));

            // Gameplay basic
            Add(new EnterGamePlayCreatePlayerSystem(contexts));
            Add(new PlayerInputSystem(contexts));
            Add(new MovementSystem(contexts));
            Add(new BoundsClampSystem(contexts));

            // Views
            Add(new ViewCreationSystem(contexts));
            Add(new ViewSyncSystem(contexts));

            // Asteroids & game over
            Add(new AsteroidSpawnSystem(contexts));
            Add(new OffscreenDespawnSystem(contexts));
            Add(new CollisionSystem(contexts));
            Add(new ReturnToMenuOnGameOverSystem(contexts));
            Add(new CleanupWorldOnEnterMenuSystem(contexts));

            // Pause time scale
            Add(new ApplyPauseTimeScaleSystem(contexts));
        }
    }
}


