using Entitas;
using UnityEngine;
using UnityEngine.UI;

namespace Astrostrafer.UI
{
    public sealed class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _startButton;

        private Contexts _contexts;

        private void Awake()
        {
            _contexts = Contexts.sharedInstance;
            if (_startButton != null)
            {
                _startButton.onClick.AddListener(OnStartClicked);
            }
        }

        private void OnDestroy()
        {
            if (_startButton != null)
            {
                _startButton.onClick.RemoveListener(OnStartClicked);
            }
        }

        private void Update()
        {
            bool show = _contexts.game.isAstrostraferECSMainMenuState;
            if (_panel != null && _panel.activeSelf != show)
            {
                _panel.SetActive(show);
            }
        }

        private void OnStartClicked()
        {
            var e = _contexts.input.CreateEntity();
            e.isAstrostraferECSInputStartPressed = true;
        }
    }
}


