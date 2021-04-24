using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {
	void Awake() {
		SetUIActive(false);
		PlayerHealth.onPlayerDied += OnPlayerDied;
	}

	void OnDestroy() {
		PlayerHealth.onPlayerDied -= OnPlayerDied;
	}

	void OnPlayerDied(PlayerHealth playerHealth) {
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
