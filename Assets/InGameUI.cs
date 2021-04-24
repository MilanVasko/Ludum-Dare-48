using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour {
	public Text health;

	void Awake() {
		PlayerCharacter.onPlayerTakenDamage += OnPlayerTakenDamage;
	}

	void OnDestroy() {
		PlayerCharacter.onPlayerTakenDamage -= OnPlayerTakenDamage;
	}

	void Start() {
		health.text = FindObjectOfType<PlayerCharacter>().GetComponent<Health>().startingHealth.ToString();
	}

	void OnPlayerTakenDamage(PlayerCharacter playerCharacter, int previousHealth, int currentHealth) {
		health.text = currentHealth.ToString();
	}
}
