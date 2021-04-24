using UnityEngine;

public class FollowCamera : MonoBehaviour {
    public Transform followTarget;
    public Vector3 offset;
    public float fixedY;

    void LateUpdate() {
        Vector3 newPosition = followTarget.position + offset;
        newPosition.y = fixedY;
        transform.position = newPosition;
    }
}
