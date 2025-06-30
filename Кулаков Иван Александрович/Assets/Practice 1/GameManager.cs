using Unity.VisualScripting;

using UnityEditor;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
	public IGameState CurrentGameState { get; private set; }

	private void Awake()
	{
		if(Instance != null)
		{
			Debug.LogWarning($"You try to create second Instance of GameManager. " +
				$"{gameObject} has been destroyed with second GameManager. " +
				$"Please use GameManager.Instance to take GameManager.");
			Destroy(gameObject);
			return;
		}
		else
			Instance = this;
	}

	public void ToPauseState()
	{
		SwitchGameToState(new Pause());
		Debug.Log("State set to Pause");
	}

	public void ToGameplayState()
	{
		SwitchGameToState(new GamePlay());
		Debug.Log("State set to Gameplay");
	}

	public void ToMainMenuState()
	{
		SwitchGameToState(new MainMenu());
		Debug.Log("State set to MainMenu");
	}

	public void SwitchGameToState(IGameState _targetStare)
	{
		if (CurrentGameState.GetType() == _targetStare.GetType())
			Debug.LogWarning($"Current GameState already in {CurrentGameState.GetType()}. " +
				$"If you wanted to update the state - use GameManager.Instance.CurrentGameState.UpdateState()");
		else
		{
			CurrentGameState.ExitState();
			CurrentGameState = _targetStare;
			CurrentGameState.EnterState();
		}
	}
}
