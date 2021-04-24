using UnityEngine;

public class GameDirector : MonoBehaviour {
	public PlayerCharacter playerCharacter;
	public float speedUpPerSecond;
	float elapsedTime = 0.0f;

	void Update() {
		elapsedTime += Time.deltaTime;
		playerCharacter.forwardSpeed += speedUpPerSecond * Time.deltaTime;
	}
}
