using UnityEngine;

public class GameController : MonoBehaviour {
    private GameSystems gameSystems;

    private void Start() {
        Contexts contexts = Contexts.sharedInstance;
        gameSystems = new GameSystems(contexts);
        gameSystems.Initialize();
    }

    private void Update() {
        gameSystems.Execute();
    }
}
