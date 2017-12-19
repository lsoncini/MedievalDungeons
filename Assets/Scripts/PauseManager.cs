using UnityEngine;

public class PauseManager : MonoBehaviour {
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject shopPanel;

    private GameManager gameManager;

    private void Start() {
        gameManager = GameManager.instance;
    }
    void Update() {
        if (gameManager.IsGameOn()) {
            if (Input.GetKeyDown(KeyCode.P)) {
                if (Time.timeScale != 0) {
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
        shopPanel.SetActive(false);
    }
}