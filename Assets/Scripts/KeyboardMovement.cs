using UnityEngine;

public class KeyboardMovement : MonoBehaviour {

    public float defaultSpeed = 1f;
    public float speed = 1f;
    private float t = 0;

    private void Start() {
        speed = defaultSpeed;
    }

    void Update () {
        if (t <= 0) {
            speed = defaultSpeed;
            GetComponent<SpriteRenderer>().color = Color.white;
        } else {
            t -= Time.deltaTime;
        }
        Vector3 movingDir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            movingDir.y = -1;
        } else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            movingDir.y = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            movingDir.x = -1;
        } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            movingDir.x = 1;
        }

        if (movingDir.magnitude == 0)
            return;

        this.SetRotation(movingDir);
        transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * speed);
    }

    private void SetRotation(Vector3 movingDir) {
        if (movingDir.magnitude == 0)
            return;

        int rotationAngle = 0;

        if (movingDir.x == 0) {
            if (movingDir.y == 1) {
                rotationAngle = 180;
            } else if (movingDir.y == -1) {
                rotationAngle = 0;
            }
        } else {
            if (movingDir.y == 1) {
                rotationAngle = 135;
            } else if (movingDir.y == -1) {
                rotationAngle = 45;
            } else {
                rotationAngle = 90;
            }
            rotationAngle = (int) movingDir.x * rotationAngle;
        }
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationAngle));
    }

    public void AlterSpeed(float speed, float time) {
        this.speed = speed;
        t = time;
        GetComponent<SpriteRenderer>().color = new Color(1f, 0.4f, 0.4f);
    }
}
