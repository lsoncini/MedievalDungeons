using UnityEngine;

public class PauseManager : MonoBehaviour {
    [SerializeField] private GameObject pausePanel;

    private GameManager manager;

    private void Start() {
        manager = GameManager.instance;
    }

    void Update() {
        if (manager.IsGameOn()) {
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