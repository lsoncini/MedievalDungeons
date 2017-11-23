using UnityEngine;

public class ArrowHandler : MonoBehaviour {

    public float speed = 2;
    private Vector3 initialPos;

	void Start () {
        initialPos = transform.position;
    }
	
	void Update () {
        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed);
        if((transform.position - initialPos).magnitude >= 2.5) {
            Destroy(gameObject);
        }
	}
}
