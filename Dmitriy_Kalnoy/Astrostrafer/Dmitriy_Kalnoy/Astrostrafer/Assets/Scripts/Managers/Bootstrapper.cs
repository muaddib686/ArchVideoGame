using Entitas;
using UnityEngine;

namespace Astrostrafer
{
    public sealed class Bootstrapper : MonoBehaviour
    {
        [Header("View Prefabs")]
        [SerializeField] private Transform _viewRoot;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _asteroidPrefab;
        private Contexts _contexts;
        private Systems _systems;

        private void Awake()
        {
            _contexts = Contexts.sharedInstance;
            if (_viewRoot == null)
            {
                var go = new GameObject("ViewRoot");
                _viewRoot = go.transform;
            }
            Astrostrafer.View.ViewServices.Initialize(_viewRoot, _playerPrefab, _asteroidPrefab);
            _systems = new Astrostrafer.ECS.GameFeature(_contexts);
            _systems.Initialize();
            Debug.Log($"Bootstrapper initialized. ViewRoot={_viewRoot?.name}, PlayerPrefab={(_playerPrefab ? _playerPrefab.name : "NULL")}, AsteroidPrefab={(_asteroidPrefab ? _asteroidPrefab.name : "NULL")}");
        }

        private void Update()
        {
            UpdateInput();
            _systems.Execute();
            _systems.Cleanup();
            ClearFrameInputFlags();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.DeactivateReactiveSystems();
                _systems.ClearReactiveSystems();
                _systems.TearDown();
                _systems = null;
            }
        }

        private void UpdateInput()
        {
            var input = _contexts.input;

            var axisEntity = input.GetGroup(InputMatcher.AstrostraferECSInputHorizontalAxis).GetSingleEntity();
            if (axisEntity == null)
            {
                axisEntity = input.CreateEntity();
                axisEntity.AddAstrostraferECSInputHorizontalAxis(0f);
            }
            float raw = UnityEngine.Input.GetAxisRaw("Horizontal");
            axisEntity.ReplaceAstrostraferECSInputHorizontalAxis(Mathf.Clamp(raw, -1f, 1f));

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space) || UnityEngine.Input.GetKeyDown(KeyCode.Return))
            {
                var e = input.CreateEntity();
                e.isAstrostraferECSInputStartPressed = true;
            }
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape) || UnityEngine.Input.GetKeyDown(KeyCode.P))
            {
                var e = input.CreateEntity();
                e.isAstrostraferECSInputPausePressed = true;
            }
        }

        private void ClearFrameInputFlags()
        {
            var input = _contexts.input;
            var pressed = input.GetGroup(InputMatcher.AnyOf(InputMatcher.AstrostraferECSInputStartPressed, InputMatcher.AstrostraferECSInputPausePressed)).GetEntities();
            foreach (var e in pressed)
            {
                e.Destroy();
            }
        }
    }
}


