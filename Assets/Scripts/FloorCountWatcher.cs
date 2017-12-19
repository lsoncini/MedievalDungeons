using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorCountWatcher : MonoBehaviour {

    private GameManager gameManager;
    private Text text;

	void Start () {
        gameManager = GameManager.instance;
        text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        text.text = string.Format("FLOOR: {0}", gameManager.GetLevelsWon() + 1);
	}
}
