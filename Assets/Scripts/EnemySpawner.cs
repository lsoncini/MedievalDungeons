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
    private GameObject[] enemies;

    public float randomXmin = -0.2f;
    public float randomXmax = 0.2f;
    public float randomYmin = -0.2f;
    public float randomYmax = 0.2f;

    private float zAngle=0f;

    private void Start() {
        t = spawnTime;
        enemies = new GameObject[maxEnemies];
        zAngle = transform.eulerAngles.z;
    }
    private void OnTriggerStay2D(Collider2D collision) {
        t += Time.deltaTime;
        if(collision.tag == target.tag) {
            if (t >= spawnTime && enemyCount < maxEnemies) {
                GameObject newGO = UnityEngine.Object.Instantiate(enemy) as GameObject;
                newGO.name = String.Format("{0}-{1}", enemy.name, enemyCount++);
                newGO.transform.parent = transform;
                newGO.transform.localPosition = new Vector3(UnityEngine.Random.Range(randomXmin, randomXmax),
                                                            UnityEngine.Random.Range(randomYmin, randomYmax),
                                                            0);
                if (newGO.GetComponent<TargetFollower>() != null) {
                    newGO.GetComponent<TargetFollower>().target = target;
                }
                if (newGO.GetComponentInChildren<ArrowHandler>() != null) {
                    newGO.GetComponentInChildren<ArrowHandler>().zAngle = zAngle - 90f;
                }
                t = 0;
                enemies[enemyCount - 1] = newGO;
            }
        }
    }

    public void DestroyEnemies() {
        foreach(GameObject go in enemies) {
            Destroy(go);
            enemyCount = 0;
            t = spawnTime;
        }
    }
}
