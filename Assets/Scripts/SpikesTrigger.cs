using UnityEngine;

public class SpikesTrigger : MonoBehaviour {
	public int amountOfDamage;

	void OnTriggerEnter(Collider other) {
		Health health = other.GetComponentInParent<Health>();
		if (health != null) {
			health.TakeDamage(amountOfDamage);
		}
	}
}
