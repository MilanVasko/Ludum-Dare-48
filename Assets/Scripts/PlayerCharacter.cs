using UnityEngine;

public class PlayerCharacter : MonoBehaviour {
	public Vector3 constantSpeed;
	public CharacterController characterController;

	void Update() {
		characterController.SimpleMove(constantSpeed);
	}

	void OnControllerColliderHit(ControllerColliderHit hit) {
		if (hit.collider.isTrigger) {
			Debug.Log(hit.collider.isTrigger);
		}
	}
}
