using System.Collections.Generic;
using UnityEngine;

public class CorridorSpawner : MonoBehaviour {
    const float CORRIDOR_LENGTH = 10.0f;

    public PlayerCharacter playerCharacter;
    public Corridor corridor;
    public int corridorsToSpawn;

    private Queue<Corridor> spawnedCorridors = new Queue<Corridor>();

	Vector3 firstSpawnLocation = Vector3.back * 2 * CORRIDOR_LENGTH;
    int spawnedCorridorsCount = 0;
    int previousCorridorID;

	void Start() {
        for (int i = 1; i <= corridorsToSpawn; i++) {
            spawnedCorridors.Enqueue(SpawnCorridor(i));
            spawnedCorridorsCount++;
        }

        previousCorridorID = CalculateCurrentPlayerCorridorID();
	}

	void Update() {
        int currentCorridorID = CalculateCurrentPlayerCorridorID();
        if (previousCorridorID != currentCorridorID) {
            previousCorridorID = currentCorridorID;
            Corridor lastCorridor = spawnedCorridors.Dequeue();
            Destroy(lastCorridor.gameObject);

            spawnedCorridors.Enqueue(SpawnCorridor(++spawnedCorridorsCount));
		}
    }

    Corridor SpawnCorridor(int id) {
        return Instantiate(corridor, firstSpawnLocation + Vector3.forward * id * CORRIDOR_LENGTH, Quaternion.identity);
	}

    int CalculateCurrentPlayerCorridorID() {
        return (int)(playerCharacter.transform.position.z / CORRIDOR_LENGTH);
	}
}
