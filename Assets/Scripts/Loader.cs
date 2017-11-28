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
            GameObject menuMusic = GameObject.Find("MenuBackgroundMusic");
            if(menuMusic != null) {
                MusicController mc = menuMusic.GetComponent<MusicController>();
                if (mc != null) {
                    mc.FadeOut();
                }
            }
            GameObject data = GameObject.Find("MenuData");
            if(data != null) {
                menuData = data.GetComponent<MenuData>();
                mapGenerator.difficulty = menuData.difficulty;
                mapGenerator.dungeonSize = menuData.dungeonSize;
                mapGenerator.seed = menuData.seedValue;
                Destroy(menuData.gameObject);
            }
            gameManager.gameLostPanel = gameLostPanel;
            gameManager.nextLevelPanel = nextLevelPanel;
            gameManager.itemMissingInfo = itemMissingInfo;
            gameManager.mapGenerator = mapGenerator;
            gameManager = Instantiate(gameManager);
        }
    }
}