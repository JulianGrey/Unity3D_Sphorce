using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LivesManager : MonoBehaviour {
    GameControl gameControlScript;
    Text text;

	void Awake() {
        text = GetComponent<Text>();
	}

    void Update() {
        if(gameControlScript != GameObject.Find("Level")) {
            if(GameObject.Find("Level") != null) {
                gameControlScript = GameObject.Find("Level").GetComponent<GameControl>();
            }
        }
        if(gameControlScript != null) {
            text.text = "Lives left: " + gameControlScript.livesLeft;
        }
    }
}
