using UnityEngine;

public class Heart : MonoBehaviour, ISelfDestructor {
	public Transform heartMesh;

	public float minFloatAnimationSpeed;
	public float maxFloatAnimationSpeed;
	public float minRotateAnimationSpeed;
	public float maxRotateAnimationSpeed;
	public float floatDistance;

	public int amountToHeal;

	float floatAnimationSpeed;
	float rotateAnimationSpeed;

	float floatInputX = 0.0f;
	float baseY;

	void Awake() {
		floatAnimationSpeed = Random.Range(minFloatAnimationSpeed, maxFloatAnimationSpeed);
		rotateAnimationSpeed = Random.Range(minRotateAnimationSpeed, maxRotateAnimationSpeed);
		baseY = heartMesh.localPosition.y;
	}

	void Update() {
		floatInputX += Time.deltaTime * floatAnimationSpeed;

		Vector3 newLocalPosition = heartMesh.localPosition;
		newLocalPosition.y = baseY + Mathf.Sin(floatInputX) * floatDistance;
		heartMesh.localPosition = newLocalPosition;

		heartMesh.Rotate(Vector3.forward, rotateAnimationSpeed * Time.deltaTime);
	}

	void OnTriggerEnter(Collider other) {
		Health health = other.GetComponentInParent<Health>();
		if (health != null) {
			if (health.Heal(amountToHeal)) {
				SelfDestruct();
			}
		}
	}

	public void SelfDestruct() {
		Destroy(gameObject);
	}
}
