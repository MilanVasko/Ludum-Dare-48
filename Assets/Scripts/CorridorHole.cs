using UnityEngine;

public class CorridorHole : MonoBehaviour {
	public int amountOfDamage;
	public Transform respawnPlace;

	void OnTriggerEnter(Collider other) {
		Health health = other.GetComponentInParent<Health>();
		if (health != null) {
			health.TakeDamage(amountOfDamage);
		}

		IRespawner respawner = other.GetComponentInParent<IRespawner>();
		if (respawner != null) {
			respawner.Respawn(respawnPlace);
		}
	}
}
