using UnityEngine;

public class Spikes : MonoBehaviour, ISelfDestructor {
	public int laneCount;
	public Transform spikes;

	void Awake() {
		int spaceIndex = Random.Range(0, laneCount);
		for (int i = 1; i < laneCount; i++) {
			if (i == spaceIndex) {
				continue;
			}
			Transform spikes = Instantiate(this.spikes, transform);
			spikes.localPosition = Vector3.right * i;
		}
		if (spaceIndex == 0) {
			Destroy(spikes.gameObject);
		}
	}

	public void SelfDestruct() {
		Destroy(gameObject);
	}
}
