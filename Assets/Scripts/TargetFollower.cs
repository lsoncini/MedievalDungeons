using UnityEngine;

public class TargetFollower : MonoBehaviour {

    public float moveSpeed = 0.1f;
    private GameObject target;

    private void Start() {
        target = GameObject.Find("Skeleton");
    }

    void Update() {
        Vector3 dir = target.transform.position - transform.position;
        transform.position += dir.normalized * Time.deltaTime * moveSpeed;
        Quaternion rotation = Quaternion.LookRotation(transform.forward, dir.normalized);
        transform.rotation = rotation;
    }
}
