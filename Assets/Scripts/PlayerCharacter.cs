using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour {
	public int minLane;
	public int maxLane;
	public float changeLanesSpeed;

	public Vector3 constantSpeed;
	public CharacterController characterController;
	int currentLane;
	int targetLane;
	Vector3 currentSpeed;

	void Awake() {
		targetLane = currentLane = CalculateCurrentLane();
		Debug.Log("Current lane detected to be " + currentLane);
		currentSpeed = constantSpeed;
	}

	void FixedUpdate() {
		currentSpeed += Physics.gravity * Time.deltaTime;
		float xDiff = (targetLane - transform.position.x) * changeLanesSpeed;
		currentSpeed.x = xDiff;

		if ((characterController.Move(currentSpeed * Time.deltaTime) & CollisionFlags.Below) != 0) {
			currentSpeed.y = 0;
			currentSpeed += Physics.gravity * Time.deltaTime;
		}
	}

	public void OnLeft(InputAction.CallbackContext callbackContext) {
		if (callbackContext.performed) {
			targetLane = Mathf.Clamp(targetLane - 1, minLane, maxLane);
		}
	}

	public void OnRight(InputAction.CallbackContext callbackContext) {
		if (callbackContext.performed) {
			targetLane = Mathf.Clamp(targetLane + 1, minLane, maxLane);
		}
	}

	int CalculateCurrentLane() {
		return Mathf.RoundToInt(transform.position.x);
	}
}
