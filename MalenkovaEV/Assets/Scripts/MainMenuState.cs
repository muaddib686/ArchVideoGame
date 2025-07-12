using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : IGameState
{
    public void EnterState()
    {
        Debug.Log("Вход в MainMenu");
        Time.timeScale = 0f; // Останавливаем время (если нужно)
                             // Здесь можно включить UI меню
        GameObject.Find("MainMenuPanel").SetActive(true);
        Cursor.visible = true;
    }

    public void ExitState()
    {
        Debug.Log("Выход из MainMenu");
        GameObject.Find("MainMenuPanel").SetActive(false);
        Time.timeScale = 1f; // Возобновляем время
        // Здесь можно отключить UI меню
    }

    public void UpdateState()
    {
        // Логика обновления (если нужно)
    }
}

