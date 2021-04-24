using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {
	public int startingHealth;
	int currentHealth;
	public UnityEvent onDeath;

	void Awake() {
		if (startingHealth <= 0) {
			throw new UnityException("Starting health must be greater than 0");
		}
		currentHealth = startingHealth;
	}

	public void TakeDamage(int amount) {
		currentHealth -= amount;
		if (currentHealth <= 0) {
			onDeath.Invoke();
		}
	}
}
