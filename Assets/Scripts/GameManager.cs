using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public int timeForLevel = 0;

    private bool isGameOn = true;
    private int levelsWon = 0;
    private int timeLeft;
    private float t = 0;

    [SerializeField] public GameObject gameLostPanel;
    [SerializeField] public GameObject nextLevelPanel;

    private void Awake() {
        levelsWon = 0;
        if (instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(gameObject);
        }
        NewLevel();
    }

    private void Update() {
        t += Time.deltaTime;
        if (t >= 1) {
            TakeTime(1);
            t = 0;
        }
        if(timeLeft <= 0) {
            GameLost();
        }
    }

    private void NewLevel() {
        isGameOn = true;
        timeLeft = timeForLevel;
    }

    public void AddTime(int timeToAdd) {
        timeLeft += timeToAdd;
    }

    public void TakeTime(int timeToTake) {
        if(timeLeft >= timeToTake) {
            timeLeft -= timeToTake;
        } else {
            timeLeft = 0;
        }
    }

    public bool IsGameOn() {
        return isGameOn;
    }

    public void GameLost() {
        isGameOn = false;
        gameLostPanel.SetActive(true);
    }

    public void LevelWon() {
        isGameOn = false;
        levelsWon++;
        nextLevelPanel.SetActive(true);
    }
}
