using UnityEngine;

public class BehindCharacterTrigger : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		ISelfDestructor selfDestructor = other.GetComponentInParent<ISelfDestructor>();
		if (selfDestructor != null) {
			selfDestructor.SelfDestruct();
		}
	}
}
