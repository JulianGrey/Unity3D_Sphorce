using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    public AudioClip[] gameSounds;

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
}
