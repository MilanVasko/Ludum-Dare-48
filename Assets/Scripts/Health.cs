using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {
	public int startingHealth;
	int currentHealth;
	bool dead = false;
	public UnityEvent<int, int> onTakenDamage;
	public UnityEvent onDeath;

	void Awake() {
		if (startingHealth <= 0) {
			throw new UnityException("Starting health must be greater than 0");
		}
		currentHealth = startingHealth;
	}

	public void TakeDamage(int amount) {
		if (dead) {
			return;
		}

		currentHealth -= amount;
		onTakenDamage?.Invoke(currentHealth + amount, currentHealth);

		if (currentHealth <= 0) {
			dead = true;
			onDeath.Invoke();
		}
	}
}
