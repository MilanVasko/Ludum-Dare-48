using UnityEngine;

public class SpikesSpawner : MonoBehaviour {
	public Spikes spikes;

	public void OnNextCorridorReached(CorridorSpawner corridorSpawner, int currentCorridorID, int lastCorridorID) {
		Instantiate(spikes, corridorSpawner.CalculateCorridorPosition(lastCorridorID), Quaternion.identity);
	}
}
