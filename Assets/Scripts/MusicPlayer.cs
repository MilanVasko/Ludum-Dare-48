using UnityEngine;

public class MusicPlayer : MonoBehaviour {
	public AudioSource audioSource;

	public AudioClip start;
	public AudioClip[] middle;
	public AudioClip end;

	void Awake() {
		audioSource.volume = PlayerSettings.musicVolume;
		PlayerSettings.onMusicVolumeChanged += OnMusicVolumeChanged;
	}

	void OnDestroy() {
		PlayerSettings.onMusicVolumeChanged -= OnMusicVolumeChanged;
	}

	void OnMusicVolumeChanged(float currentValue) {
		audioSource.volume = currentValue;
	}

	void Start() {
		audioSource.PlayOneShot(start);
		audioSource.PlayScheduled(AudioSettings.dspTime + start.length);
	}

	public void Update() {
		if (!audioSource.isPlaying) {
			audioSource.PlayOneShot(middle[Random.Range(0, middle.Length)]);
		}
	}
}
