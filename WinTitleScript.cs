using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinTitleScript : MonoBehaviour {
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
            if(gameControlScript.nextLevel == "Frontend") {
                text.text = "CONGRATULATIONS!!";
            }
            else {
                text.text = "LEVEL COMPLETE!";
            }
        }
    }
}
