﻿using UnityEngine;

public class ItemPicker : MonoBehaviour {

    private int keysPicked = 0;
    private int mapsPicked = 0;
    public int keysNeeded = 3;
    public int mapsNeeded = 0;

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Key")) {
            Destroy(collision.gameObject);
            keysPicked++;
            audioSource.Play();
        }
        if (collision.gameObject.CompareTag("Map")) {
            Destroy(collision.gameObject);
            mapsPicked++;
            audioSource.Play();
        }
        TrapHandler trapHandler = collision.gameObject.GetComponent<TrapHandler>();
        if(trapHandler != null) {
            GetComponent<KeyboardMovement>().AlterSpeed(trapHandler.speed, trapHandler.time);
            if (trapHandler.destroyOnCollision) {
                Destroy(collision.gameObject);
            }
        }
    }

    public bool HasAllItemsNeeded() {
        return (keysPicked >= keysNeeded) && (mapsPicked >= mapsNeeded);
    }

    public int GetKeysPicked() {
        return keysPicked;
    }

    public int GetMapsPicked() {
        return mapsPicked;
    }

    public void Reset() {
        keysPicked = 0;
        mapsPicked = 0;
    }
}
