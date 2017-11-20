using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public GameObject target;
    public float spawnTime = 1f;
    public int maxEnemies = 5;
    private float t;
    private int enemyCount = 0;

    private void Start() {
        t = spawnTime;
    }
    private void OnTriggerStay2D(Collider2D collision) {
        t += Time.deltaTime;
        if(collision.tag == target.tag) {
            if (t >= spawnTime && enemyCount < maxEnemies) {
                GameObject newGO = UnityEngine.Object.Instantiate(enemy) as GameObject;
                newGO.name = String.Format("{0}-{1}", enemy.name, enemyCount++);
                newGO.transform.parent = transform;
                newGO.transform.localPosition = new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f),
                                                            UnityEngine.Random.Range(-0.2f, 0.2f),
                                                            0);
                newGO.GetComponent<TargetFollower>().target = target;
                print(String.Format("spawning +{0}",enemyCount-1));
                t = 0;
            }
        }
    }
}
