using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelController : MonoBehaviour {

	public void NextLevel() {
        GameManager.instance.NewLevel();
    }
}
