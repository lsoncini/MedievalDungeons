using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour {

    public float moveSpeed = 0.1f;
    public GameObject target;
    // Update is called once per frame
    void Update() {
        transform.position += (target.transform.position - this.transform.position) * Time.deltaTime * moveSpeed;
    }
}
