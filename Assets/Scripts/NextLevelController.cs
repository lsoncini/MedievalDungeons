using UnityEngine;

public class NextLevelController : MonoBehaviour {

	public void NextLevel() {
        GameManager.instance.NextLevel();
    }
}
