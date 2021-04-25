using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour {
	public Slider soundsVolumeSlider;

	void Awake() {
		soundsVolumeSlider.value = PlayerSettings.soundsVolume;
		SetUIActive(FindObjectOfType<GameDirector>().isPaused);

		PlayerSettings.onSoundsVolumeChanged += OnSoundsVolumeChanged;
		GameDirector.onPauseStateChanged += OnPauseStateChanged;
	}

	void OnDestroy() {
		GameDirector.onPauseStateChanged -= OnPauseStateChanged;
		PlayerSettings.onSoundsVolumeChanged -= OnSoundsVolumeChanged;
	}

	public void OnAudioVolumeSliderChanged(float currentValue) {
		PlayerSettings.soundsVolume = currentValue;
	}

	void OnSoundsVolumeChanged(float currentValue) {
		soundsVolumeSlider.value = currentValue;
	}

	void OnPauseStateChanged(bool paused) {
		SetUIActive(paused);
	}

	void SetUIActive(bool active) {
		gameObject.SetActive(active);
	}
}
