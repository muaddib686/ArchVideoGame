using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private IGameState currentState;
    public string currentStateName {
        get {
            return currentState.GetType().Name;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        currentState = new MainMenu();
        currentState.EnterState();
    }

    public void ChangeState(IGameState newState)
    {
        currentState.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(currentStateName);
        currentState.UpdateState();
    }
}
