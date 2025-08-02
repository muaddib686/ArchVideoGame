using UnityEngine;

/* Static configuration class that holds all game constants and configuration values.
 * This replaces the GameConfigComponent and consolidates all magic numbers from systems.
 */
public static class Config {
    // Asteroid configuration
    public static float AsteroidSpawnRate = 2.0f; // seconds between spawns
    public static float AsteroidVelocity = 5.0f; // units per second
    public static float AsteroidAngleRange = 30.0f; // degrees from vertical
    public static Vector2 AsteroidSize = new Vector2(1.05f, 0.9f);

    // Player configuration
    public static float PlayerSpeed = 8.0f; // units per second
    public static Vector2 PlayerSize = new Vector2(1f, 1f);
    public static Vector2 PlayerStartPosition = new Vector2(0, -3.5f);

    // Screen/world configuration
    public static float ScreenWidth = 10.0f; // game world width
    public static float ScreenHeight = 10.0f; // game world height
    public static float OutOfBoundsTolerance = 2.0f; // tolerance for entities flying off screen

    // Sprite paths
    public static string AsteroidSpritePath = "Sprites/asteroid";
    public static string PlayerSpritePath = "Sprites/spaceship";
}