using UnityEngine;

public class GamePlayState : IGameState
{
    private int counter = 0;

    public void EnterState()
    {
        Debug.Log("Entering Game Play State");
        GameManager.Instance.uiService.EnablePlayingPanel();
    }

    public void ExitState()
    {
        Debug.Log("Exiting Game Play State");
        GameManager.Instance.uiService.DisablePlayingPanel();
    }

    public void UpdateState()
    {
        // We do nothing in the game play state
        counter++;
        GameManager.Instance.uiService.SetCounterValue(counter);
    }
}