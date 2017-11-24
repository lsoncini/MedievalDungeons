using UnityEngine;

public class DoorController : MonoBehaviour {

    private GameManager gameManager;
    private Camera mainCamera;
    private GameObject itemMissingInfo;

    private void Start() {
        gameManager = GameManager.instance;
        itemMissingInfo = gameManager.itemMissingInfo;
        itemMissingInfo.SetActive(false);
        // itemMissingInfo.transform.Rotate(0, 0, transform.eulerAngles.z + 90);
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Skeleton")) {
            ItemPicker ip = collision.gameObject.GetComponent<ItemPicker>();
            if (ip.HasAllItemsNeeded()) {
                gameManager.LevelWon();
            } else {
                itemMissingInfo.transform.position = mainCamera.WorldToScreenPoint(collision.transform.position);
                itemMissingInfo.transform.Translate(0, 50, 0);
                itemMissingInfo.SetActive(true);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Skeleton")) {
            itemMissingInfo.SetActive(false);
        }
    }
}
