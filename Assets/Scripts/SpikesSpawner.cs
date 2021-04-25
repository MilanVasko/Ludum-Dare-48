using UnityEngine;

public class SpikesSpawner : MonoBehaviour {
	public Spikes spikes;
	public float spawnChance;

	public void OnNextCorridorReached(CorridorSpawner corridorSpawner, int currentCorridorID, int lastCorridorID) {
		if (Random.Range(0.0f, 1.0f) < spawnChance) {
			Instantiate(spikes, corridorSpawner.CalculateCorridorPosition(lastCorridorID), Quaternion.identity);
		}
	}
}
