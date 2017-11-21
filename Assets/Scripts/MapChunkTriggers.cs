using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapChunkTriggers : MonoBehaviour {

    public CameraManager cameraManager;

    private void OnTriggerExit2D(Collider2D collision) {
        foreach(EnemySpawner es in this.GetComponentsInChildren<EnemySpawner>()) {
            es.DestroyEnemies();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        cameraManager.MoveTo(this.transform.position);
    }
}
