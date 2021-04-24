using System.Collections.Generic;
using UnityEngine;

public class CorridorSpawner : MonoBehaviour {
    public Corridor corridor;
    public int corridorsToSpawn;

    private Queue<Corridor> spawnedCorridors = new Queue<Corridor>();

    Vector3 firstSpawnLocation = Vector3.back * 2 * CorridorHandler.CORRIDOR_LENGTH;
    int spawnedCorridorsCount = 0;

    void Start() {
        for (int i = 1; i <= corridorsToSpawn; i++) {
            spawnedCorridors.Enqueue(SpawnCorridor(i));
            spawnedCorridorsCount++;
        }
    }

    public void OnNextCorridorReached() {
        Corridor lastCorridor = spawnedCorridors.Dequeue();
        Destroy(lastCorridor.gameObject);

        spawnedCorridors.Enqueue(SpawnCorridor(++spawnedCorridorsCount));
    }

    Corridor SpawnCorridor(int id) {
        return Instantiate(corridor, firstSpawnLocation + Vector3.forward * id * CorridorHandler.CORRIDOR_LENGTH, Quaternion.identity);
    }
}
