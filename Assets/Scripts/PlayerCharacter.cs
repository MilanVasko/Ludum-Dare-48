using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : MonoBehaviour {
	public static event Action<PlayerCharacter> onPlayerDied;

	public int laneCount;
	public float changeLanesSpeed;
	public float jumpSpeed;

	public Vector3 constantSpeed;
	public CharacterController characterController;
	int currentLane;
	int targetLane;
	bool wantsToJump = false;
	Vector3 currentSpeed;

	void Awake() {
		characterController.enabled = false;
		Vector3 newPosition = transform.position;
		newPosition.x = laneCount / 2;
		transform.position = newPosition;
		characterController.enabled = true;

		targetLane = currentLane = CalculateCurrentLane();
		Debug.Log("Current lane detected to be " + currentLane);
		currentSpeed = constantSpeed;
	}

	void FixedUpdate() {
		currentSpeed += Physics.gravity * Time.deltaTime;
		float xDiff = (targetLane - transform.position.x) * changeLanesSpeed;
		currentSpeed.x = xDiff;

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
			targetLane = Mathf.Clamp(targetLane + 1, 0, laneCount);
		}
	}

	public void OnJump(InputAction.CallbackContext callbackContext) {
		if (callbackContext.performed) {
			wantsToJump = true;
		}
	}

	public void OnDeath() {
		onPlayerDied?.Invoke(this);
	}

	int CalculateCurrentLane() {
		return Mathf.RoundToInt(transform.position.x);
	}
}
