using System;
using UnityEngine;

public class GameDirector : MonoBehaviour {
	public static event Action<float> onTimeChanged;
	public static event Action<bool> onPauseStateChanged;

	float _elapsedTime = 0.0f;
	public float elapsedTime { get { return _elapsedTime; } }

	public bool isPaused { get { return Time.timeScale == 0; } }

	void Start() {
		onTimeChanged?.Invoke(_elapsedTime);
	}

	void Update() {
		_elapsedTime += Time.deltaTime;
		onTimeChanged?.Invoke(_elapsedTime);
	}

	public void OnPausePressed() {
		if (isPaused && FindObjectOfType<PlayerHealth>().isDead) {
			return;
		}
		TogglePause();
	}

	void TogglePause() {
		SetPause(!isPaused);
	}

	public void SetPause(bool pause) {
		Time.timeScale = pause ? 0 : 1;
		onPauseStateChanged?.Invoke(pause);
	}
}
