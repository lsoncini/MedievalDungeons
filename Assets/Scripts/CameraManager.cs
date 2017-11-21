using UnityEngine;

public class CameraManager : MonoBehaviour {
    private Vector3 desiredPosition;
    public float panSpeed = 2f;

    private void Start() {
        desiredPosition = transform.position;
    }

    public void MoveTo(Vector2 position) {
        desiredPosition = new Vector3(position.x, position.y, transform.position.z);
    }

    public void Update() {
        if (desiredPosition.Equals(transform.position)) {
            return;
        }
        transform.position += (desiredPosition - transform.position) * Time.deltaTime * panSpeed;
    }
}