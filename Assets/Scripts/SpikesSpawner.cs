using UnityEngine;

public class SpikesSpawner : MonoBehaviour {
	public Spikes spikes;
	public float maxSpawnDistance;
	public float minSpaceBetweenSpikes;
	public float maxSpaceBetweenSpikes;
	public int spikesToSpawn;

	void Start() {
		for (int i = 1; i <= spikesToSpawn; i++) {
		}
	}

	public void OnNextCorridorReached() {
		Debug.Log("Spawn spikes");
	}
}
