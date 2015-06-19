using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    public AudioClip[] gameSounds;
    private GameControl gameControlScript;

    void Update() {
        if(gameControlScript != GameObject.Find("Level")) {
            if(GameObject.Find("Level") != null) {
                gameControlScript = GameObject.Find("Level").GetComponent<GameControl>();
            }
        }
        if(gameControlScript != null) {
            if(gameControlScript.gameOver) {
                GetComponent<AudioSource>().Stop();
            }
        }
    }
}
