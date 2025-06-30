
public interface IGameState
{
    /** Called when the state is entered. */
    void EnterState();

    /** Called when the state is exited. */
    void ExitState();

    /** Called every frame while the state is active. */
    void UpdateState();
}