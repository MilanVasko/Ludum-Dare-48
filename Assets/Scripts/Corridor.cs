using UnityEngine;

public class Corridor : MonoBehaviour, ISelfDestructor {
	public void SelfDestruct() {
		Destroy(gameObject);
	}

	public bool isCorridorWithHole {
		get {
			foreach (Transform child in transform) {
				if (child.name == "CorridorWithHole") {
					return true;
				}
			}
			return false;
		}
	}
}
