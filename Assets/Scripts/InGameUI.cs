using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour {
	public Text elapsedTime;
	public Text health;

	void Awake() {
		GameDirector.onTimeChanged += OnTimeChanged;
		PlayerHealth.onPlayerTakenDamage += OnPlayerTakenDamage;
		PlayerHealth.onPlayerHealed += OnPlayerHealed;
	}

	void OnDestroy() {
		PlayerHealth.onPlayerHealed -= OnPlayerHealed;
		PlayerHealth.onPlayerTakenDamage -= OnPlayerTakenDamage;
		GameDirector.onTimeChanged -= OnTimeChanged;
	}

	void Start() {
		health.text = FindObjectOfType<PlayerHealth>().GetComponent<Health>().startingHealth.ToString();
	}

	void OnTimeChanged(float currentTime) {
		elapsedTime.text = Util.FormatElapsedTime(currentTime);
	}

	void OnPlayerTakenDamage(PlayerHealth playerHealth, int previousHealth, int currentHealth) {
		health.text = currentHealth.ToString();
	}

	void OnPlayerHealed(PlayerHealth playerHealth, int previousHealth, int currentHealth) {
		health.text = currentHealth.ToString();
	}
}
