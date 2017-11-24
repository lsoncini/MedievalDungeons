using UnityEngine;

public class PauseManager : MonoBehaviour {
    [SerializeField] private GameObject pausePanel;

    private GameManager gameManager;

    private void Start() {
        gameManager = GameManager.instance;
    }
    void Update() {
        if (gameManager.IsGameOn()) {
            if (Input.GetKeyDown(KeyCode.P)) {
                if (!pausePanel.activeInHierarchy) {
                    PauseGame();
                } else {
                    ContinueGame();
                }
            }
        }
    }
    public void PauseGame() {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void ContinueGame() {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}