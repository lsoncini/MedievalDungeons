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
        cameraManager = GameObject.Find("Main Camera").GetComponent<CameraManager>();
        skeleton = GameObject.Find("Skeleton");
        NewLevel();
    }

    private void Update() {
        if (!isGameOn) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
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

    private void NewLevel() {
        isGameOn = true;
        timeLeft = timeForLevel;
        mapGenerator.Generate();
        putPlayerAtRandomPosition();
    }

    private void putPlayerAtRandomPosition() {
        Vector3 randomPos = mapGenerator.GetRandomPlayerPositionInMap();
        print(randomPos);
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
        isGameOn = false;
        gameLostPanel.SetActive(true);
    }

    public void LevelWon() {
        isGameOn = false;
        levelsWon++;
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
}
