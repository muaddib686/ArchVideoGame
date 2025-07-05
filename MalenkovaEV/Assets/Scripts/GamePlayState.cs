using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayState : IGameState
{
    public void EnterState()
    {
        Debug.Log("Вход в GamePlay");
        Time.timeScale = 1f; // Игра работает
                             // Здесь можно включить игровые элементы
        GameObject.Find("PausePanel").SetActive(false); // Скрываем паузу
        Cursor.visible = false;
    }

    public void ExitState()
    {
        Debug.Log("Выход из GamePlay");
        // Здесь можно отключить игровые элементы
    }

    public void UpdateState()
    {
        // Логика обновления (например, проверка нажатия кнопки паузы)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.PauseGame(); // Используем метод GameManager
        }
    }
}
