using UnityEngine;

public class PlayerSounds : MonoBehaviour {
	public AudioSource audioSource;

	public AudioClip[] hurt;
	public AudioClip[] jump;
	public AudioClip[] powerup;
	public AudioClip[] died;

	void Awake() {
		OnSoundsVolumeChanged(PlayerSettings.soundsVolume);

		PlayerHealth.onPlayerTakenDamage += OnPlayerTakenDamage;
		PlayerHealth.onPlayerDied += OnPlayerDied;
		PlayerCharacter.onPlayerJump += OnPlayerJump;
		PlayerSettings.onSoundsVolumeChanged += OnSoundsVolumeChanged;
	}

	void OnDestroy() {
		PlayerCharacter.onPlayerJump -= OnPlayerJump;
		PlayerHealth.onPlayerDied -= OnPlayerDied;
		PlayerHealth.onPlayerTakenDamage -= OnPlayerTakenDamage;
		PlayerSettings.onSoundsVolumeChanged -= OnSoundsVolumeChanged;
	}

	void OnPlayerTakenDamage(PlayerHealth playerHealth, int previousHealth, int currentHealth) {
		PlayRandomClip(hurt);
	}

	void OnPlayerDied(PlayerHealth obj) {
		PlayRandomClip(died);
	}

	void OnPlayerJump(PlayerCharacter playerCharacter) {
		PlayRandomClip(jump);
	}

	void PlayRandomClip(AudioClip[] clips) {
		audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
	}

	void OnSoundsVolumeChanged(float newVolume) {
		audioSource.volume = newVolume;
	}
}
