using UnityEngine;

public class Health : MonoBehaviour {
	public int startingHealth;
	int currentHealth;

	protected bool dead = false;

	void Awake() {
		currentHealth = startingHealth;
	}

	public void TakeDamage(int amount) {
		if (!ShouldTakeDamage(amount)) {
			return;
		}

		currentHealth -= amount;
		OnTakenDamage(currentHealth + amount, currentHealth);

		if (currentHealth <= 0) {
			dead = true;
			OnDeath();
		}
	}

	protected virtual bool ShouldTakeDamage(int amount) {
		return !dead;
	}

	protected virtual void OnTakenDamage(int previousHealth, int currentHealth) { }
	protected virtual void OnDeath() { }
}
