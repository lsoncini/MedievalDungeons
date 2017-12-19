using UnityEngine;
using UnityEngine.UI;

public class MenuData : MonoBehaviour {

    public Difficulty difficulty = Difficulty.Easy;
    public DungeonSize dungeonSize = DungeonSize.Small;
    public GameMode gameMode = GameMode.Training;
    public int seedValue = 12345;
    public InputController input;

    void Start() {
        DontDestroyOnLoad(this);
    }

    public void SetDifficulty(int difficulty) {
        this.difficulty = (Difficulty)difficulty;
    }

    public void SetDungeonSize(int dungeonSize) {
        this.dungeonSize = (DungeonSize)dungeonSize;
    }

    public void SetSeedValue() {
        this.seedValue = input.getValue();
    }

    public void SetGameMode(int gameMode) {
        this.gameMode = (GameMode)gameMode;
    }
}
