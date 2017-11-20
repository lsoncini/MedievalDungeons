using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour {

    private int keysPicked = 0;
    private int mapsPicked = 0;
    public int keysNeeded = 3;
    public int mapsNeeded = 0;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Key")) {
            Destroy(collision.gameObject);
            keysPicked++;
            print(keysPicked);
        }
        if (collision.gameObject.CompareTag("Map")) {
            Destroy(collision.gameObject);
            mapsPicked++;
            print(mapsPicked);
        }
    }

    public bool HasAllItemsNeeded() {
        return (keysPicked >= keysNeeded) && (mapsPicked >= mapsNeeded);
    }

    public void Reset() {
        keysPicked = 0;
        mapsPicked = 0;
    }
}
