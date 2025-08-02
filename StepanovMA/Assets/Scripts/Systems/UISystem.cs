using Entitas;
using UnityEngine;
using UnityEngine.UI;

/* System that glues Unity UI for main menu and game over menu with ECS. */
public class UISystem : IInitializeSystem, IExecuteSystem {
    private readonly Contexts contexts;

    // UI References
    private GameObject mainMenuPanel;
    private GameObject gameOverPanel;
    private Button playButton;
    private Button restartButton;

    public UISystem(Contexts contexts) {
        this.contexts = contexts;
    }

    public void Initialize() {
        FindUIGameObjects();
        SetupButtonListeners();

        // Get game state entity
        var gameStateEntities = contexts.game.GetGroup(GameMatcher.GameState).GetEntities();
        if (gameStateEntities.Length == 0) {
            // Create game state entity if it doesn't exist
            var gameStateEntity = contexts.game.CreateEntity();
            gameStateEntity.AddGameState(GameStateComponent.State.MainMenu);
        }

        // Show main menu initially
        ShowMainMenu();
    }

    public void Execute() {
        var gameStateEntities = contexts.game.GetGroup(GameMatcher.GameState).GetEntities();
        if (gameStateEntities.Length == 0) return;
        var gameStateEntity = gameStateEntities[0];

        var gameState = gameStateEntity.gameState;

        // Update UI visibility based on game state
        switch (gameState.currentState) {
            case GameStateComponent.State.MainMenu:
                ShowMainMenu();
                break;
            case GameStateComponent.State.Playing:
                ShowGameUI();
                break;
            case GameStateComponent.State.GameOver:
                ShowGameOverMenu();
                break;
        }
    }

    private void FindUIGameObjects() {
        mainMenuPanel = GameObject.Find("MainMenuPanel");
        gameOverPanel = GameObject.Find("GameOverPanel");

        if (mainMenuPanel == null) {
            Debug.LogWarning("MainMenuPanel not found in the scene!");
        }

        if (gameOverPanel == null) {
            Debug.LogWarning("GameOverPanel not found in the scene!");
        }

        playButton = GameObject.Find("PlayButton")?.GetComponent<Button>();
        restartButton = GameObject.Find("RestartButton")?.GetComponent<Button>();

        if (playButton == null) {
            Debug.LogWarning("PlayButton not found in the scene!");
        }

        if (restartButton == null) {
            Debug.LogWarning("RestartButton not found in the scene!");
        }
    }

    private void SetupButtonListeners() {
        if (playButton != null) {
            playButton.onClick.AddListener(OnPlayButtonClicked);
        }

        if (restartButton != null) {
            restartButton.onClick.AddListener(OnRestartButtonClicked);
        }
    }

    private void OnPlayButtonClicked() {
        // Create UI button entity for play button
        var uiButtonEntity = contexts.game.CreateEntity();
        uiButtonEntity.AddUIButton(UIButtonComponent.ButtonType.Play);

        Debug.Log("Play button clicked!");
    }

    private void OnRestartButtonClicked() {
        // Create UI button entity for restart button
        var uiButtonEntity = contexts.game.CreateEntity();
        uiButtonEntity.AddUIButton(UIButtonComponent.ButtonType.Restart);

        Debug.Log("Restart button clicked!");
    }

    private void ShowMainMenu() {
        // TODO: replace this logic with something scalable when game scope grows
        if (mainMenuPanel != null) {
            mainMenuPanel.SetActive(true);
        }

        if (gameOverPanel != null) {
            gameOverPanel.SetActive(false);
        }
    }

    private void ShowGameUI() {
        // TODO: replace this logic with something scalable when game scope grows
        if (mainMenuPanel != null) {
            mainMenuPanel.SetActive(false);
        }

        if (gameOverPanel != null) {
            gameOverPanel.SetActive(false);
        }
    }

    private void ShowGameOverMenu() {
        // TODO: replace this logic with something scalable when game scope grows
        if (mainMenuPanel != null) {
            mainMenuPanel.SetActive(false);
        }

        if (gameOverPanel != null) {
            gameOverPanel.SetActive(true);
        }
    }
}