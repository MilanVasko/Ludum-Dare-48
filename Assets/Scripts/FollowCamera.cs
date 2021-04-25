using System.Collections;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public Transform followTarget;
    public float distanceFromTarget;

	public float shakeDuration;
	public float shakeSpeed;
	public float shakeDistance;

	float originalPositionX;
	float originalPositionY;

	void Awake() {
		originalPositionX = transform.position.x;
		originalPositionY = transform.position.y;

		PlayerHealth.onPlayerTakenDamage += OnPlayerTakenDamage;
	}

	void OnDestroy() {
		PlayerHealth.onPlayerTakenDamage -= OnPlayerTakenDamage;
	}

	void OnPlayerTakenDamage(PlayerHealth playerHealth, int previousHealth, int currentHealth) {
		StartCoroutine(ShakeAnimation());
	}

	IEnumerator ShakeAnimation() {
		float speedX = Random.Range(1.0f, 2.0f);
		float speedY = Random.Range(1.0f, 2.0f);

		for (float currentTime = 0.0f; currentTime < shakeDuration; currentTime += Time.deltaTime) {
			Vector3 newPosition = transform.position;
			newPosition.x = originalPositionX + Mathf.Sin(currentTime * speedX * shakeSpeed) * shakeDistance;
			newPosition.y = originalPositionY + Mathf.Sin(currentTime * speedY * shakeSpeed) * shakeDistance;
			transform.position = newPosition;

			yield return null;
		}

		transform.position = new Vector3(originalPositionX, originalPositionY, transform.position.z);
	}

	void LateUpdate() {
        Vector3 newPosition = transform.position;
        newPosition.z = followTarget.position.z - distanceFromTarget;
        transform.position = newPosition;
    }
}
