using UnityEngine;

public class Flamethrower : MonoBehaviour, ISelfDestructor {
	public AudioSource audioSource;
	public AudioClip[] fireClips;
	public new Collider collider;
	public new ParticleSystem particleSystem;

	public float minThrowDuration;
	public float maxThrowDuration;
	public float minCooldown;
	public float maxCooldown;

	public int amountOfDamage;

	public bool throwingFlames;

	float throwDuration;
	float cooldown;
	float currentTime;

	void Awake() {
		PlayerSettings.onSoundsVolumeChanged += OnSoundsVolumeChanged;

		throwDuration = Random.Range(minThrowDuration, maxThrowDuration);
		cooldown = Random.Range(minCooldown, maxCooldown);

		if (throwingFlames) {
			StartThrowingFlames();
		} else {
			StopThrowingFlames();
		}
	}

	void OnDestroy() {
		PlayerSettings.onSoundsVolumeChanged -= OnSoundsVolumeChanged;
	}

	void Start() {
		OnSoundsVolumeChanged(PlayerSettings.soundsVolume);
	}

	void OnSoundsVolumeChanged(float newVolume) {
		audioSource.volume = newVolume;
	}

	void Update() {
		if (throwingFlames) {
			currentTime -= Time.deltaTime;
			if (currentTime <= 0.0f) {
				throwingFlames = !throwingFlames;
				StopThrowingFlames();
			}
		} else {
			currentTime -= Time.deltaTime;
			if (currentTime <= 0.0f) {
				throwingFlames = !throwingFlames;
				StartThrowingFlames();
			}
		}
	}

	void StartThrowingFlames() {
		currentTime = throwDuration;
		collider.enabled = true;
		audioSource.PlayOneShot(fireClips[Random.Range(0, fireClips.Length)]);
		particleSystem.Play();
	}

	void StopThrowingFlames() {
		currentTime = cooldown;
		collider.enabled = false;
		audioSource.Stop();
		particleSystem.Stop();
	}

	public void SelfDestruct() {
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other) {
		Health health = other.GetComponentInParent<Health>();
		if (health != null) {
			health.TakeDamage(amountOfDamage);
		}
	}
}
