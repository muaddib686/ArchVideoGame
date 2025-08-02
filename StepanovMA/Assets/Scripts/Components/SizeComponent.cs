using Entitas;
using UnityEngine;

/* Component that holds the size of the entity. Used for collision detection. */
[Game]
public class SizeComponent : IComponent {
    public Vector2 value;
}