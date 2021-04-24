using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour {
	public int laneCount;
	public float changeLanesSpeed;
	public float jumpSpeed;

	public float forwardSpeed;
	public CharacterController characterController;
	int currentLane;
	int targetLane;
	bool wantsToJump = false;
	Vector3 currentSpeed;

	void Awake() {
		Vector3 newPosition = transform.position;
		newPosition.x = laneCount / 2;
		TeleportTo(newPosition);
		Debug.Log("Current lane detected to be " + currentLane);
		currentSpeed = new Vector3(0.0f, 0.0f, forwardSpeed);
	}

	public void TeleportTo(Vector3 newPosition) {
		characterController.enabled = false;
		transform.position = newPosition;
		targetLane = currentLane = CalculateCurrentLane();
		characterController.enabled = true;
	}

	public void ResetSpeed() {
		currentSpeed = Vector3.zero;
	}

	void FixedUpdate() {
		currentSpeed += Physics.gravity * Time.deltaTime;
		float xDiff = (targetLane - transform.position.x) * changeLanesSpeed;
		currentSpeed.x = xDiff;
		currentSpeed.z = forwardSpeed;

		if (wantsToJump) {
			wantsToJump = false;
			if (characterController.isGrounded) {
				currentSpeed.y = jumpSpeed;
			}
		}

		if ((characterController.Move(currentSpeed * Time.deltaTime) & CollisionFlags.Below) != 0) {
			currentSpeed.y = 0;
			currentSpeed += Physics.gravity * Time.deltaTime;
		}
	}

	public void OnLeft(InputAction.CallbackContext callbackContext) {
		if (callbackContext.performed) {
			targetLane = Mathf.Clamp(targetLane - 1, 0, laneCount);
		}
	}

	public void OnRight(InputAction.CallbackContext callbackContext) {
		if (callbackContext.performed) {
			targetLane = Mathf.Clamp(targetLane + 1, 0, laneCount - 1);
		}
	}

	public void OnJump(InputAction.CallbackContext callbackContext) {
		if (callbackContext.performed) {
			wantsToJump = true;
		}
	}

	int CalculateCurrentLane() {
		return Mathf.RoundToInt(transform.position.x);
	}
}
