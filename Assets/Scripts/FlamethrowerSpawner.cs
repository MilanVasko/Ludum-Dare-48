using UnityEngine;

public class FlamethrowerSpawner : MonoBehaviour {
	public Flamethrower flamethrower;
	public float spawnChance;
	public int startAtCorridor;
	public Vector3 offset;
	public int laneCount;

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
			Debug.Log("Spawning flamethrower at " + corridorID);

			Vector3 position = corridorSpawner.CalculateCorridorPosition(corridorID) + offset;
			Quaternion rotation = Quaternion.identity;
			if (Random.Range(0.0f, 1.0f) < 0.5f) {
				// spawn on the right
				position += Vector3.right * (laneCount - 1);
			} else {
				// spawn on the left
				rotation.SetLookRotation(Vector3.back);
			}

			Flamethrower f = Instantiate(flamethrower, position, rotation);
			f.throwingFlames = Random.Range(0.0f, 1.0f) < 0.5f;
		}
	}
}
