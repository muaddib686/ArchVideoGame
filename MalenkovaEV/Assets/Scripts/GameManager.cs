using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    // Singleton
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    // Состояния
    public IGameState MainMenuState { get; private set; }
    public IGameState GamePlayState { get; private set; }
    public IGameState PauseState { get; private set; }

    private IGameState _currentState;

    private void Awake()
    {
        // Singleton паттерн
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject); // Чтобы не уничтожался при загрузке сцен

        // Инициализируем состояния
        MainMenuState = new MainMenuState();
        GamePlayState = new GamePlayState();
        PauseState = new PauseState();
    }

    private void Start()
    {
        // Начинаем с главного меню
        ChangeState(MainMenuState);
    }

    private void Update()
    {
        _currentState?.UpdateState();
    }
    public void ResumeFromPause()
    {
        ChangeState(GamePlayState); // Возвращаемся в игровой режим
    }
    // Метод для смены состояния
    public void ChangeState(IGameState newState)
    {
        _currentState?.ExitState();
        _currentState = newState;
        _currentState.EnterState();
    }
    public void PauseGame()
    {
        Debug.Log("PauseGame()");
        ChangeState(PauseState);
    }
    // Методы для удобного переключения
    public void GoToMainMenu() => ChangeState(MainMenuState);
    public void StartGame() => ChangeState(GamePlayState);
  
    public void ResumeGame() => ChangeState(GamePlayState);
}
