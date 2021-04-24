using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {
	void Awake() {
		SetUIActive(false);
		PlayerCharacter.onPlayerDied += OnPlayerDied;
	}

	void OnDestroy() {
		PlayerCharacter.onPlayerDied -= OnPlayerDied;
	}

	void OnPlayerDied(PlayerCharacter playerCharacter) {
		SetUIActive(true);
	}

	public void OnTryAgainPressed() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
	}

	void SetUIActive(bool active) {
		gameObject.SetActive(active);
		Time.timeScale = active ? 0 : 1;
	}
}
