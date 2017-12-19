using System;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    private CameraManager cameraManager;
    public MapGenerator mapGenerator;
    public GameObject skeleton;
    public int timeForLevel = 0;

    private bool isGameOn = true;
    private int levelsWon = 0;
    private int timeLeft;
    private float t = 0;

    [SerializeField] public GameObject gameLostPanel;
    [SerializeField] public GameObject nextLevelPanel;
    [SerializeField] public GameObject itemMissingInfo;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if(instance != this) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        levelsWon = 0;
        NewLevel();
    }

    private void Update() {
        if (isGameOn) {
            t += Time.deltaTime;
            if (t >= 1) {
                TakeTime(1);
                t = 0;
            }
            if (timeLeft <= 0) {
                GameLost();
            }
        }
    }

    public void NewLevel() {
        cameraManager = GameObject.Find("Main Camera").GetComponent<CameraManager>();
        skeleton = GameObject.Find("Skeleton");
        ItemPicker ip = skeleton.GetComponent<ItemPicker>();
        if(ip != null) {
            ip.Reset();
        }

        mapGenerator.SetItemDropRates();
        mapGenerator.Generate();
        putPlayerAtRandomPosition();

        setItemsToPickInLevel();
        setTimeForLevel();
        timeLeft = timeForLevel;
        Time.timeScale = 1;
        isGameOn = true;
    }

    private void setTimeForLevel() {
        int baseTime = 0;
        switch (mapGenerator.dungeonSize) {
            case DungeonSize.Small:
                baseTime = 90; //1:30
                if(mapGenerator.difficulty != Difficulty.Easy) {
                    baseTime += 30;
                }
                break;
            case DungeonSize.Normal:
                baseTime = 150; //2:30
                break;
            case DungeonSize.Big:
                baseTime = 240; //4:00
                break;
        }
        timeForLevel = baseTime;
    }

    private void setItemsToPickInLevel() {
        ItemPicker ip = skeleton.GetComponent<ItemPicker>();
        int keysNeeded = mapGenerator.GetKeyCount();
        print(keysNeeded);
        int mapsNeeded = mapGenerator.GetMapCount();
        print(mapsNeeded);
        switch (mapGenerator.difficulty) {
            case Difficulty.Easy:
                keysNeeded = UnityEngine.Random.Range(keysNeeded / 3, keysNeeded / 2 + 1);
                mapsNeeded = UnityEngine.Random.Range(mapsNeeded / 3, mapsNeeded / 2 + 1);
                break;
            case Difficulty.Medium:
                keysNeeded = UnityEngine.Random.Range(keysNeeded / 2, keysNeeded * 2 / 3 + 1);
                mapsNeeded = UnityEngine.Random.Range(mapsNeeded / 2, mapsNeeded * 2 / 3 + 1);
                break;
            default:            //HARD mode asks for all items in map
                break;
        }
        ip.keysNeeded = keysNeeded;
        ip.mapsNeeded = mapsNeeded;
    }

    public void NextLevel() {
        mapGenerator.seed += 1;
        levelsWon++;
        NewLevel();
    }

    private void putPlayerAtRandomPosition() {
        Vector3 randomPos = mapGenerator.GetRandomPlayerPositionInMap();
        skeleton.transform.position = randomPos;
        PanCameraToChunk();
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
        Time.timeScale = 0;
        isGameOn = false;
        gameLostPanel.SetActive(true);
    }

    public void LevelWon() {
        Time.timeScale = 0;
        isGameOn = false;
        nextLevelPanel.SetActive(true);
    }

    public void PanCameraToChunk() {
        MapChunk currentChunk = mapGenerator.GetChunkAt(skeleton.transform.position);
        if(currentChunk != null) {
            cameraManager.MoveTo(currentChunk.transform.position);
        }
    }

    public int GetTimeLeft() {
        return timeLeft;
    }

    public int GetLevelsWon() {
        return levelsWon;
    }
}
