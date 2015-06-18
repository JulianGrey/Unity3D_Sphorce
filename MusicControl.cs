using UnityEngine;
using System.Collections;

public class MusicControl : MonoBehaviour {
    
    void Awake() {
        // Needed so the audio gameObject persists between levels
        DontDestroyOnLoad(transform.gameObject);
    }
}
