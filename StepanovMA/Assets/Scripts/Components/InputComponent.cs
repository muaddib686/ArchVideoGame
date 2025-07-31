using Entitas;

/* Component that holds the input from the player.
 * We can only move horizontally.
 */
[Game]
public class InputComponent : IComponent {
    public float horizontalInput;
}
