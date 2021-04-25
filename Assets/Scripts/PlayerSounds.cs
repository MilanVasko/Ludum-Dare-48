using UnityEngine;

public class PlayerSounds : MonoBehaviour {
	public AudioSource audioSource;

	public AudioClip[] hurt;
	public AudioClip[] jump;
	public AudioClip[] powerup;
	public AudioClip[] died;
	public AudioClip[] heal;

	void Awake() {
		PlayerHealth.onPlayerTakenDamage += OnPlayerTakenDamage;
		PlayerHealth.onPlayerDied += OnPlayerDied;
		PlayerHealth.onPlayerHealed += OnPlayerHealed;
		PlayerCharacter.onPlayerJump += OnPlayerJump;
		PlayerSettings.onSoundsVolumeChanged += OnSoundsVolumeChanged;
	}

	void OnDestroy() {
		PlayerCharacter.onPlayerJump -= OnPlayerJump;
		PlayerHealth.onPlayerDied -= OnPlayerDied;
		PlayerHealth.onPlayerHealed -= OnPlayerHealed;
		PlayerHealth.onPlayerTakenDamage -= OnPlayerTakenDamage;
		PlayerSettings.onSoundsVolumeChanged -= OnSoundsVolumeChanged;
	}

	void Start() {
		OnSoundsVolumeChanged(PlayerSettings.soundsVolume);
	}

	void OnPlayerTakenDamage(PlayerHealth playerHealth, int previousHealth, int currentHealth) {
		PlayRandomClip(hurt);
	}

	void OnPlayerDied(PlayerHealth playerHealth) {
		PlayRandomClip(died);
	}

	void OnPlayerHealed(PlayerHealth playerHealth, int previousHealth, int currentHealth) {
		PlayRandomClip(heal);
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
