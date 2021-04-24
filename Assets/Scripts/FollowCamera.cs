using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public Transform followTarget;
    public float distanceFromTarget;

    void LateUpdate() {
        Vector3 newPosition = transform.position;
        newPosition.z = followTarget.position.z - distanceFromTarget;
        transform.position = newPosition;
    }
}
