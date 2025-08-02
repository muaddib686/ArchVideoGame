using Entitas;

/* Component that holds the game state. Basically, a simple state machine. */
[Game]
public class GameStateComponent : IComponent {
    public enum State {
        MainMenu,
        Playing,
        GameOver
    }

    public State currentState = State.MainMenu;
}