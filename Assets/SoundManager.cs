using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip firstScreenSound;
    public AudioClip gameSound;

    public KeyCode button;

    public static SoundManager instance = null;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if (Input.GetKeyDown(button)) {
            GetComponent<AudioSource>().mute = !GetComponent<AudioSource>().mute;
        }
    }

    public void LoadFirstScreen() {
        GetComponent<AudioSource>().clip = firstScreenSound;
        GetComponent<AudioSource>().Play();
    }

    public void LoadGame() {
        GetComponent<AudioSource>().clip = gameSound;
        GetComponent<AudioSource>().Play();
    }
}
