using UnityEngine;

public class MusicController : MonoBehaviour {

    private float totalVolume;
    private AudioSource audioSource;
    private bool isFadingOut = false;
    private float time;

	void Start () {
        DontDestroyOnLoad(this);
        audioSource = GetComponent<AudioSource>();
        totalVolume = audioSource.volume;
	}

    private void Update() {
        if (isFadingOut) {
            time += Time.deltaTime;
            if(time >= 1) {
                time = 0;
                audioSource.volume -= totalVolume / 3;
                if(audioSource.volume <= 0) {
                    Destroy(this);
                }
            }
        }
    }

    public void FadeOut() {
        isFadingOut = true;
    }
}
