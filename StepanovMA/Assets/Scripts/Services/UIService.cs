using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject playingPanel;
    public GameObject pauseMenuPanel;

    public Button playButton;
    public Button pauseButton;
    public Button resumeButton;
    public Button mainMenuButton;

    public TMP_Text counterText;

    public void Start()
    {
        GameManager.Instance.uiService = this;

        playButton.onClick.AddListener(GameManager.Instance.TransitionToGamePlay);
        pauseButton.onClick.AddListener(GameManager.Instance.TransitionToPause);
        resumeButton.onClick.AddListener(GameManager.Instance.TransitionToGamePlay);
        mainMenuButton.onClick.AddListener(GameManager.Instance.TransitionToMainMenu);
    }

    public void EnableMainMenuPanel()
    {
        mainMenuPanel.SetActive(true);
    }

    public void DisableMainMenuPanel()
    {
        mainMenuPanel.SetActive(false);
    }

    public void EnablePlayingPanel()
    {
        playingPanel.SetActive(true);
    }

    public void DisablePlayingPanel()
    {
        playingPanel.SetActive(false);
    }

    public void EnablePauseMenuPanel()
    {
        pauseMenuPanel.SetActive(true);
    }

    public void DisablePauseMenuPanel()
    {
        pauseMenuPanel.SetActive(false);
    }

    public void SetCounterValue(int value)
    {
        counterText.text = "Counter: " + value.ToString();
    }
}