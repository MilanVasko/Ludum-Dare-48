using UnityEngine;

public class Spikes : MonoBehaviour, ISelfDestructor {
	public int laneCount;
	public int amountOfDamage;
	public Transform spikes;
	public BoxCollider boxCollider;

	void Awake() {
		for (int i = 1; i < laneCount; i++) {
			Transform spikes = Instantiate(this.spikes, transform);
			spikes.localPosition = Vector3.right * i;
		}

		Vector3 newSize = boxCollider.size;
		newSize.x = laneCount;
		boxCollider.size = newSize;

		Vector3 newCenter = boxCollider.center;
		newCenter.x = (laneCount - 1) / 2.0f;
		boxCollider.center = newCenter;
	}

	void OnTriggerEnter(Collider other) {
		Health health = other.GetComponentInParent<Health>();
		if (health != null) {
			health.TakeDamage(amountOfDamage);
		}
	}

	public void SelfDestruct() {
		Destroy(gameObject);
	}
}
