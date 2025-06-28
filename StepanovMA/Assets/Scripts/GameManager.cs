using UnityEngine;

/*
 * GameManager is a singleton that manages the game state.
 *
 * It deliberately limits transitions by using methods, because state machine
 * should not allow extending states without updating the state machine logic.
 */
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private IGameState currentState;

    // We store state instances here becauses states, are, well, states.
    // They may store some internal data we don't know about.
    // And GamePlayState stores such data.
    private MainMenuState mainMenuState;
    private GamePlayState gamePlayState;
    private PauseState pauseState;

    // Service instances
    public UIService uiService;

    void Awake()
    {
        if (instance != null)
            throw new System.Exception("GameManager already exists");

        instance = this;

        mainMenuState = new MainMenuState();
        gamePlayState = new GamePlayState();
        pauseState = new PauseState();

        // Initial state is the main menu
        currentState = mainMenuState;
    }

    void Start()
    {
    }

    void Update()
    {
        currentState.UpdateState();
    }

    public void TransitionToMainMenu()
    {
        if (currentState == mainMenuState)
            return;
        if (currentState == gamePlayState)
            throw new System.Exception("Cannot transition to main menu from game play state");

        currentState.ExitState();
        currentState = mainMenuState;
        currentState.EnterState();
    }

    public void TransitionToGamePlay()
    {
        if (currentState == gamePlayState)
            return;

        currentState.ExitState();
        currentState = gamePlayState;
        currentState.EnterState();
    }

    public void TransitionToPause()
    {
        if (currentState == pauseState)
            return;
        if (currentState == mainMenuState)
            throw new System.Exception("Cannot transition to pause from main menu state");

        currentState.ExitState();
        currentState = pauseState;
        currentState.EnterState();
    }
}
