using System.Collections;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour, IRespawner {
	public PlayerCharacter playerCharacter;
	public float newSpeedDuration;
	public float newSpeedFactor;

	public void Respawn(Transform respawnPlace) {
		playerCharacter.TeleportTo(respawnPlace.position);
		playerCharacter.ResetSpeed();
		StartCoroutine(SlowDownTemporarily());
	}

	IEnumerator SlowDownTemporarily() {
		float originalSpeed = playerCharacter.forwardSpeed;
		float newSpeed = playerCharacter.forwardSpeed * newSpeedFactor;

		float t = 0.0f;
		while (t < newSpeedDuration) {
			t += Time.deltaTime;
			playerCharacter.forwardSpeed = Mathf.Lerp(newSpeed, originalSpeed, t / newSpeedDuration);
			yield return null;
		}

		playerCharacter.forwardSpeed = originalSpeed;
	}
}
