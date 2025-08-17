using Entitas;
using UnityEngine;
using UnityEngine.UI;

namespace Astrostrafer.UI
{
    public sealed class PauseMenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _mainMenuButton;

        private Contexts _contexts;

        private void Awake()
        {
            _contexts = Contexts.sharedInstance;
            if (_resumeButton != null) _resumeButton.onClick.AddListener(OnResume);
            if (_mainMenuButton != null) _mainMenuButton.onClick.AddListener(OnMainMenu);
        }

        private void OnDestroy()
        {
            if (_resumeButton != null) _resumeButton.onClick.RemoveListener(OnResume);
            if (_mainMenuButton != null) _mainMenuButton.onClick.RemoveListener(OnMainMenu);
        }

        private void Update()
        {
            bool show = _contexts.game.isAstrostraferECSPauseState;
            if (_panel != null && _panel.activeSelf != show)
                _panel.SetActive(show);
        }

        private void OnResume()
        {
            var e = _contexts.input.CreateEntity();
            e.isAstrostraferECSInputPausePressed = true;
        }

        private void OnMainMenu()
        {
            // Прямо выставим состояния
            _contexts.game.isAstrostraferECSPauseState = false;
            _contexts.game.isAstrostraferECSGamePlayState = false;
            _contexts.game.isAstrostraferECSMainMenuState = true;
        }
    }
}


