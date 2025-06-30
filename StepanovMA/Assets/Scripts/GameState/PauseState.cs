using UnityEngine;

public class PauseState : IGameState
{
    public void EnterState()
    {
        Debug.Log("Entering Pause State");
        GameManager.Instance.uiService.EnablePauseMenuPanel();
    }

    public void ExitState()
    {
        Debug.Log("Exiting Pause State");
        GameManager.Instance.uiService.DisablePauseMenuPanel();
    }

    public void UpdateState()
    {
        // We do nothing in the pause state
    }
}