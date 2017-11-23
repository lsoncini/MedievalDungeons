using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public float spawnTime = 1f;
    public int maxEnemies = 5;
    private float t;
    private int enemyCount = 0;
    private GameObject[] enemies;

    public float randomXmin = -0.2f;
    public float randomXmax = 0.2f;
    public float randomYmin = -0.2f;
    public float randomYmax = 0.2f;

    private void Start() {
        t = spawnTime;
        enemies = new GameObject[maxEnemies];
    }
    private void OnTriggerStay2D(Collider2D collision) {
        t += Time.deltaTime;
        if(collision.CompareTag("Skeleton")) {
            if (t >= spawnTime && enemyCount < maxEnemies) {
                GameObject newGO = Instantiate(enemy,enemy.transform.position,Quaternion.Euler(0,0,transform.eulerAngles.z - 90)) as GameObject;
                newGO.name = String.Format("{0}-{1}", enemy.name, enemyCount++);
                newGO.transform.parent = transform;
                newGO.transform.localPosition = new Vector3(UnityEngine.Random.Range(randomXmin, randomXmax),
                                                            UnityEngine.Random.Range(randomYmin, randomYmax),
                                                            0);
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
