using UnityEngine;

public class MenuData : MonoBehaviour {

    public Difficulty difficulty = Difficulty.Easy;
    public DungeonSize dungeonSize = DungeonSize.Small;

    void Start() {
        DontDestroyOnLoad(this);
    }

    public void SetDifficulty(int difficulty) {
        this.difficulty = (Difficulty)difficulty;
    }

    public void SetDungeonSize(int dungeonSize) {
        this.dungeonSize = (DungeonSize)dungeonSize;
    }
}
