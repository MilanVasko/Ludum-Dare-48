using UnityEngine;
using UnityEngine.Events;

public class CorridorSpawner : MonoBehaviour {
    public const float CORRIDOR_LENGTH = 10.0f;

    public Corridor corridor;
    public Corridor corridorWithHole;
    public int corridorsToSpawn;
    public float corridorWithHoleSpawnChance;

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
            SpawnCorridor(corridor, i);
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
        Corridor whichCorridor = (Random.Range(0.0f, 1.0f) < corridorWithHoleSpawnChance) ? corridorWithHole : corridor;
        SpawnCorridor(whichCorridor, ++spawnedCorridorsCount);
    }

    Corridor SpawnCorridor(Corridor corridor, int id) {
        return Instantiate(corridor, CalculateCorridorPosition(id), Quaternion.identity);
    }

    public int CalculateCurrentPlayerCorridorID() {
        return (int)(playerCharacter.transform.position.z / CORRIDOR_LENGTH);
    }

    public Vector3 CalculateCorridorPosition(int corridorID) {
        return firstSpawnLocation + Vector3.forward * corridorID * CORRIDOR_LENGTH;
	}
}
