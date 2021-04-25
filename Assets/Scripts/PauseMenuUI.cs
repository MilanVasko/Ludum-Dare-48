using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour {
	public Slider soundsVolumeSlider;
	public Slider musicVolumeSlider;

	void Awake() {
		soundsVolumeSlider.value = PlayerSettings.soundsVolume;
		musicVolumeSlider.value = PlayerSettings.musicVolume;

		PlayerSettings.onSoundsVolumeChanged += OnSoundsVolumeChanged;
		PlayerSettings.onMusicVolumeChanged += OnMusicVolumeChanged;
		GameDirector.onPauseStateChanged += OnPauseStateChanged;
	}

	void OnDestroy() {
		GameDirector.onPauseStateChanged -= OnPauseStateChanged;
		PlayerSettings.onMusicVolumeChanged -= OnMusicVolumeChanged;
		PlayerSettings.onSoundsVolumeChanged -= OnSoundsVolumeChanged;
	}

	void Start() {
		SetUIActive(FindObjectOfType<GameDirector>().isPaused);
	}

	public void OnSoundsVolumeSliderChanged(float currentValue) {
		PlayerSettings.soundsVolume = currentValue;
	}

	public void OnMusicVolumeSliderChanged(float currentValue) {
		PlayerSettings.musicVolume = currentValue;
	}

	void OnSoundsVolumeChanged(float currentValue) {
		soundsVolumeSlider.value = currentValue;
	}

	void OnMusicVolumeChanged(float currentValue) {
		musicVolumeSlider.value = currentValue;
	}

	void OnPauseStateChanged(bool paused) {
		SetUIActive(paused);
	}

	void SetUIActive(bool active) {
		gameObject.SetActive(active);
	}
}
