using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour {
	public Text health;

	void Awake() {
		PlayerHealth.onPlayerTakenDamage += OnPlayerTakenDamage;
	}

	void OnDestroy() {
		PlayerHealth.onPlayerTakenDamage -= OnPlayerTakenDamage;
	}

	void Start() {
		health.text = FindObjectOfType<PlayerHealth>().GetComponent<Health>().startingHealth.ToString();
	}

	void OnPlayerTakenDamage(PlayerHealth playerHealth, int previousHealth, int currentHealth) {
		health.text = currentHealth.ToString();
	}
}
