using UnityEngine;
using UnityEngine.Events;

public class CorridorSpawner : MonoBehaviour {
    public const float CORRIDOR_LENGTH = 10.0f;

    public Corridor corridor;
    public int corridorsToSpawn;

    public PlayerCharacter playerCharacter;
    public UnityEvent<CorridorSpawner, int, int> onNextCorridorReached;

    int previousCorridorID;

    Vector3 firstSpawnLocation = Vector3.back * 2 * CORRIDOR_LENGTH;
    int spawnedCorridorsCount = 0;

    void Awake() {
        previousCorridorID = CalculateCurrentPlayerCorridorID();
    }

    void Start() {
        for (int i = 1; i <= corridorsToSpawn; i++) {
            SpawnCorridor(i);
            spawnedCorridorsCount++;
        }
    }

    void Update() {
        int currentCorridorID = CalculateCurrentPlayerCorridorID();
        if (previousCorridorID != currentCorridorID) {
            previousCorridorID = currentCorridorID;
			onNextCorridorReached.Invoke(this, currentCorridorID, currentCorridorID + corridorsToSpawn);
		}
	}

    public void OnNextCorridorReached() {
        SpawnCorridor(++spawnedCorridorsCount);
    }

    Corridor SpawnCorridor(int id) {
        return Instantiate(corridor, CalculateCorridorPosition(id), Quaternion.identity);
    }

    public int CalculateCurrentPlayerCorridorID() {
        return (int)(playerCharacter.transform.position.z / CORRIDOR_LENGTH);
    }

    public Vector3 CalculateCorridorPosition(int corridorID) {
        return firstSpawnLocation + Vector3.forward * corridorID * CORRIDOR_LENGTH;
	}
}
