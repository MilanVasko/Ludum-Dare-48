using System;
using UnityEngine;

public class GameDirector : MonoBehaviour {
	public static event Action<float> onTimeChanged;
	public static event Action<bool> onPauseStateChanged;

	float elapsedTime = 0.0f;

	public bool isPaused {
		get {
			return Time.timeScale == 0;
		}
	}

	void Start() {
		onTimeChanged?.Invoke(elapsedTime);
	}

	void Update() {
		elapsedTime += Time.deltaTime;
		onTimeChanged?.Invoke(elapsedTime);
	}

	public void TogglePause() {
		SetPause(!isPaused);
	}

	public void SetPause(bool pause) {
		Time.timeScale = pause ? 0 : 1;
		onPauseStateChanged?.Invoke(pause);
	}
}
