using UnityEngine;

public class Corridor : MonoBehaviour, ISelfDestructor {
	public void SelfDestruct() {
        Destroy(gameObject);
	}
}
