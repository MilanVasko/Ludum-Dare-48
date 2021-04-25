using UnityEngine;

public class SpikesSpawner : MonoBehaviour {
	public Spikes spikes;
	public float spawnChance;
	public int startAtCorridor;

	void Awake() {
		CorridorSpawner corridorSpawner = GetComponent<CorridorSpawner>();
		for (int i = startAtCorridor; i <= corridorSpawner.corridorsToSpawn; i++) {
			SpawnAtCorridor(corridorSpawner, i);
		}
	}

	public void OnNextCorridorReached(CorridorSpawner corridorSpawner, int currentCorridorID, int lastCorridorID) {
		SpawnAtCorridor(corridorSpawner, lastCorridorID);
	}

	void SpawnAtCorridor(CorridorSpawner corridorSpawner, int corridorID) {
		if (Random.Range(0.0f, 1.0f) < spawnChance) {
			Instantiate(spikes, corridorSpawner.CalculateCorridorPosition(corridorID), Quaternion.identity);
		}
	}
}
