using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : IGameState
{
    private GameObject PausePanel;
    public void EnterState()
    {
        Debug.Log("Entering Pause State");
        Time.timeScale = 0f;
        if (PausePanel == null)
        {
            PausePanel = GameObject.Find("PausePanel");
            if (PausePanel == null)
            {
                Debug.LogError("PausePanel not found in scene!");
                return;
            }
            PausePanel.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ExitState()
    {
        Debug.Log("Exiting Pause State");
        if (PausePanel != null)
        {
            PausePanel.SetActive(false);
        }
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void UpdateState()
    {

    }
}
