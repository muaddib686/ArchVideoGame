using UnityEngine;

public class MainMenuState : IGameState
{
    public void EnterState()
    {
        Debug.Log("Entering Main Menu State");
        GameManager.Instance.uiService.EnableMainMenuPanel();
    }

    public void ExitState()
    {
        Debug.Log("Exiting Main Menu State");
        GameManager.Instance.uiService.DisableMainMenuPanel();
    }

    public void UpdateState()
    {
        // We do nothing in the main menu
    }
}