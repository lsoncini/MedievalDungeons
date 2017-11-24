using UnityEngine;

public class Loader : MonoBehaviour {
    public GameManager gameManager;
    public MapGenerator mapGenerator;
    private MenuData menuData;

    [SerializeField] private GameObject gameLostPanel;
    [SerializeField] private GameObject nextLevelPanel;
    [SerializeField] private GameObject itemMissingInfo;

    private void Awake() {
        if (GameManager.instance == null) {
            menuData = GameObject.Find("MenuData").GetComponent<MenuData>();
            if(menuData != null) {
                mapGenerator.difficulty = menuData.difficulty;
                mapGenerator.dungeonSize = menuData.dungeonSize;
            }
            gameManager.gameLostPanel = gameLostPanel;
            gameManager.nextLevelPanel = nextLevelPanel;
            gameManager.itemMissingInfo = itemMissingInfo;
            gameManager.mapGenerator = mapGenerator;
            gameManager = Instantiate(gameManager);
        }
    }
}