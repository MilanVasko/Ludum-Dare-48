using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : Health {
	public MeshRenderer meshRenderer;
	public float invincibleAfterDamageTime;

	public static event Action<PlayerHealth> onPlayerDied;
	public static event Action<PlayerHealth, int, int> onPlayerTakenDamage;

	bool isInvincibleAfterDamage = false;

	protected override bool ShouldTakeDamage(int amount) {
		return !isInvincibleAfterDamage && base.ShouldTakeDamage(amount);
	}

	protected override void OnTakenDamage(int previousHealth, int currentHealth) {
		base.OnTakenDamage(previousHealth, currentHealth);
		onPlayerTakenDamage?.Invoke(this, previousHealth, currentHealth);
		StartCoroutine(StartInvincibility());
	}

	protected override void OnDeath() {
		base.OnDeath();
		onPlayerDied?.Invoke(this);
	}

	IEnumerator StartInvincibility() {
		isInvincibleAfterDamage = true;
		Color previousColor = meshRenderer.material.color;
		meshRenderer.material.color = Color.red;

		yield return new WaitForSeconds(invincibleAfterDamageTime);

		meshRenderer.material.color = previousColor;
		isInvincibleAfterDamage = false;
	}
}
