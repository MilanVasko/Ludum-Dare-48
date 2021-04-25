using System;
using UnityEngine;

public class GameDirector : MonoBehaviour {
	public static event Action<float> onTimeChanged;

	float elapsedTime = 0.0f;

	void Start() {
		onTimeChanged?.Invoke(elapsedTime);
	}

	void Update() {
		elapsedTime += Time.deltaTime;
		onTimeChanged?.Invoke(elapsedTime);
	}
}
