using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LivesManager : MonoBehaviour {
    GameControl gameControlScript;
    Text text;

	void Awake() {
        text = GetComponent<Text>();
	}

    void Start() {
        gameControlScript = GameObject.Find("Actions").GetComponent<GameControl>();
    }

    void Update() {
        text.text = "Lives left: " + gameControlScript.livesLeft;
    }
}
