using UnityEngine;

public class MapChunkTriggers : MonoBehaviour {

    private GameManager gameManager;

    private void Start() {
        gameManager = GameManager.instance;
    }
    private void OnTriggerExit2D(Collider2D collision) {
        foreach(EnemySpawner es in this.GetComponentsInChildren<EnemySpawner>()) {
            es.DestroyEnemies();
        }
        gameManager.PanCameraToChunk();
    }
}
