using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinTextScript : MonoBehaviour {
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
                text.text = "You have completed Sphorce.\nTouch the screen to go to the main menu.";
            }
            else {
                text.text = "Touch the screen to continue to the next level";
            }
        }
    }
}
