using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {
    public GameManager gameManager;
    public GameObject skeleton;
    public MapGenerator mapGenerator;
    [SerializeField] private GameObject gameLostPanel;
    [SerializeField] private GameObject nextLevelPanel;

    private void Awake() {
        if (GameManager.instance == null) {
            gameManager = Instantiate(gameManager);
            gameManager.gameLostPanel = gameLostPanel;
            gameManager.nextLevelPanel = nextLevelPanel;
            gameManager.skeleton = skeleton;
            gameManager.mapGenerator = mapGenerator;
        }
    }
}