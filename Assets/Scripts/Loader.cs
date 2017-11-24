using UnityEngine;

public class Loader : MonoBehaviour {
    public GameManager gameManager;
    public MapGenerator mapGenerator;

    [SerializeField] private GameObject gameLostPanel;
    [SerializeField] private GameObject nextLevelPanel;
    [SerializeField] private GameObject itemMissingInfo;

    private void Awake() {
        if (GameManager.instance == null) {
            gameManager.gameLostPanel = gameLostPanel;
            gameManager.nextLevelPanel = nextLevelPanel;
            gameManager.itemMissingInfo = itemMissingInfo;
            gameManager.mapGenerator = mapGenerator;
            gameManager = Instantiate(gameManager);
        }
    }
}