using UnityEngine;

public class Health : MonoBehaviour {
	public int startingHealth;
	protected int currentHealth;

	protected bool dead = false;
	public bool isDead { get { return dead; } }

	void Awake() {
		currentHealth = startingHealth;
	}

	public bool Heal(int amount) {
		if (!ShouldHeal(amount)) {
			return false;
		}

		currentHealth += amount;
		OnHealed(currentHealth - amount, currentHealth);
		return true;
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

	protected virtual bool ShouldHeal(int amount) {
		return !dead;
	}

	protected virtual bool ShouldTakeDamage(int amount) {
		return !dead;
	}

	protected virtual void OnTakenDamage(int previousHealth, int currentHealth) { }
	protected virtual void OnHealed(int previousHealth, int currentHealth) { }
	protected virtual void OnDeath() { }
}
