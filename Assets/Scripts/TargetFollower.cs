using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour {

    public float moveSpeed = 0.1f;
    private GameObject target;

    private void Start() {
        target = GameObject.Find("Skeleton");
    }
    // Update is called once per frame
    void Update() {
        Vector3 dir = target.transform.position - this.transform.position;
        transform.position += dir.normalized * Time.deltaTime * moveSpeed;
        Quaternion rotation = Quaternion.LookRotation(this.transform.forward, dir.normalized);
        transform.rotation = rotation;
    }
}
