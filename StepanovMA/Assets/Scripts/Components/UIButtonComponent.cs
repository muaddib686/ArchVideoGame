using Entitas;

/* Component that represents a UI button click event. */
[Game]
public class UIButtonComponent : IComponent {
    public enum ButtonType {
        Play,
        Restart
    }

    public ButtonType buttonType;
}