using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {
    public GameObject gameManager;
    [SerializeField] private GameObject gameLostPanel;
    [SerializeField] private GameObject nextLevelPanel;

    private void Awake() {
        if (GameManager.instance == null) {
            Instantiate(gameManager);
            GameManager.instance.gameLostPanel = gameLostPanel;
            GameManager.instance.nextLevelPanel = nextLevelPanel;
        }
    }
}