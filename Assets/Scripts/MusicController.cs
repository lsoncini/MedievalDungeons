using UnityEngine;

public class MusicController : MonoBehaviour {

    private float totalVolume;
    private AudioSource audioSource;
    private bool isFadingOut = false;
    private float time;
    private int count = 0;

	void Start () {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        totalVolume = audioSource.volume;
	}

    private void Update() {
        if (isFadingOut) {
            time += Time.deltaTime;
            if(time >= 1) {
                count++;
                time = 0;
                audioSource.volume /= 3;
                if(count == 2) {
                    Destroy(gameObject);
                }
            }
        }
    }

    public void FadeOut() {
        isFadingOut = true;
        count = 0;
    }
}
