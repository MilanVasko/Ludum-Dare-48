using UnityEngine;
using UnityEngine.Events;

public class CorridorHandler : MonoBehaviour {
    public const float CORRIDOR_LENGTH = 10.0f;

    public PlayerCharacter playerCharacter;
    public UnityEvent<int> onNextCorridorReached;

    int previousCorridorID;

    void Awake() {
        previousCorridorID = CalculateCurrentPlayerCorridorID();
    }

    void Update() {
        int currentCorridorID = CalculateCurrentPlayerCorridorID();
        if (previousCorridorID != currentCorridorID) {
            previousCorridorID = currentCorridorID;
            onNextCorridorReached.Invoke(currentCorridorID);
        }
    }

    public int CalculateCurrentPlayerCorridorID() {
        return (int)(playerCharacter.transform.position.z / CORRIDOR_LENGTH);
    }
}
