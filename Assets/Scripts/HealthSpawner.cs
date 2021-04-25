using UnityEngine;

public class HealthSpawner : MonoBehaviour {
	public int minCorridorsPassed;
	public int maxCorridorsPassed;
	public Vector3 spawnOffset;
	public Heart heartPrefab;
	public int laneCount;

	int currentCorridorsPassedTarget;

	void Awake() {
		currentCorridorsPassedTarget = Random.Range(minCorridorsPassed, maxCorridorsPassed);
		CorridorSpawner corridorSpawner = GetComponent<CorridorSpawner>();
		for (int i = 0; i <= corridorSpawner.corridorsToSpawn; i++) {
			SpawnAtCorridor(corridorSpawner, i);
		}
	}

	public void OnNextCorridorReached(CorridorSpawner corridorSpawner, int currentCorridorID, int lastCorridorID) {
		SpawnAtCorridor(corridorSpawner, lastCorridorID);
	}

	void SpawnAtCorridor(CorridorSpawner corridorSpawner, int corridorID) {
		--currentCorridorsPassedTarget;
		if (currentCorridorsPassedTarget <= 0) {
			currentCorridorsPassedTarget = Random.Range(minCorridorsPassed, maxCorridorsPassed);
			SpawnHeart(corridorSpawner.CalculateCorridorPosition(corridorID) + spawnOffset);
		}
	}

	void SpawnHeart(Vector3 position) {
		int lane = Random.Range(0, laneCount);
		Instantiate(heartPrefab, position + (Vector3.right * lane), Quaternion.identity);
		Debug.Log("Spawning heart on lane " + lane);
	}
}
