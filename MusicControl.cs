using UnityEngine;
using System.Collections;

public class MusicControl : MonoBehaviour {
    
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
}
