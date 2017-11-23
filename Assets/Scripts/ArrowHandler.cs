using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHandler : MonoBehaviour {

    public float speed = 2;
    private Vector3 initialPos;

	// Use this for initialization
	void Start () {
        initialPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * speed);
        if((transform.position - initialPos).magnitude >= 2.5) {
            Destroy(gameObject);
        }
	}
}
