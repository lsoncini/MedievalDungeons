using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    private GameManager gameManager;
    public Text timer;
    public Text keysPickedLabel;
    public Text mapsPickedLabel;
    public Image allKeysPicked;
    public Image allMapsPicked;

    private ItemPicker skeleton;

    void Start () {
        gameManager = GameManager.instance;
        skeleton = GameObject.Find("Skeleton").GetComponent<ItemPicker>();
	}
	
	void Update () {
        int time = gameManager.GetTimeLeft();
        var minutes = time / 60;
        var seconds = time % 60;
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        int keysNeeded = skeleton.keysNeeded;
        int mapsNeeded = skeleton.mapsNeeded;

        int keysPicked = skeleton.GetKeysPicked();
        int mapsPicked = skeleton.GetMapsPicked();

        if(mapsPicked >= mapsNeeded) {
            allMapsPicked.gameObject.SetActive(true);
            mapsPickedLabel.gameObject.SetActive(false);
        } else {
            allMapsPicked.gameObject.SetActive(false);
            mapsPickedLabel.gameObject.SetActive(true);
            mapsPickedLabel.text = string.Format("{0}/{1}", mapsPicked, mapsNeeded);
        }

        if (keysPicked >= keysNeeded) {
            allKeysPicked.gameObject.SetActive(true);
            keysPickedLabel.gameObject.SetActive(false);
        } else {
            allKeysPicked.gameObject.SetActive(false);
            keysPickedLabel.gameObject.SetActive(true);
            keysPickedLabel.text = string.Format("{0}/{1}", keysPicked, keysNeeded);
        }

    }
}
