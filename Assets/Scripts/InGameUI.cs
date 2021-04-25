using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour {
	public Text elapsedTime;
	public Text health;

	void Awake() {
		GameDirector.onTimeChanged += OnTimeChanged;
		PlayerHealth.onPlayerTakenDamage += OnPlayerTakenDamage;
	}

	void OnDestroy() {
		PlayerHealth.onPlayerTakenDamage -= OnPlayerTakenDamage;
		GameDirector.onTimeChanged -= OnTimeChanged;
	}

	void Start() {
		health.text = FindObjectOfType<PlayerHealth>().GetComponent<Health>().startingHealth.ToString();
	}

	void OnTimeChanged(float currentTime) {
		int totalSeconds = (int)currentTime;
		int totalMinutes = totalSeconds / 60;
		int totalHours = totalMinutes / 60;

		int milliseconds = ((int)(currentTime * 1000)) % 1000;
		int seconds = totalSeconds % 60;
		int minutes = totalMinutes % 60;
		int hours = totalHours % 60;

		elapsedTime.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00") + "." + milliseconds.ToString("000");
	}

	void OnPlayerTakenDamage(PlayerHealth playerHealth, int previousHealth, int currentHealth) {
		health.text = currentHealth.ToString();
	}
}
