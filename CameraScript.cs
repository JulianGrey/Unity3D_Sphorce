using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
    private GameControl gameControlScript;

    private Color cyan = new Color(86.0f/255.0f, 175.0f/255.0f, 159.0f/255.0f, 1.0f);
    private Color yellow = new Color(182.0f/255.0f, 183.0f/255.0f, 88.0f/255.0f, 1.0f);
    private Color green = new Color(103.0f/255.0f, 198.0f/255.0f, 88.0f/255.0f, 1.0f);
    private Color blue = new Color(71.0f/255.0f, 104.0f/255.0f, 227.0f/255.0f, 1.0f);
    private Color orange = new Color(225.0f/255.0f, 129.0f/255.0f, 71.0f/255.0f, 1.0f);
    private Color purple = new Color(122.0f/255.0f, 57.0f/255.0f, 123.0f/255.0f, 1.0f);

    private int rndNum;

    void Start() {
        rndNum = Random.Range(0, 6);
        GetComponent<Camera>().clearFlags = CameraClearFlags.Color;

        switch(rndNum) {
            case 0:
                GetComponent<Camera>().backgroundColor = cyan;
                break;
            case 1:
                GetComponent<Camera>().backgroundColor = yellow;
                break;
            case 2:
                GetComponent<Camera>().backgroundColor = green;
                break;
            case 3:
                GetComponent<Camera>().backgroundColor = blue;
                break;
            case 4:
                GetComponent<Camera>().backgroundColor = orange;
                break;
            case 5:
                GetComponent<Camera>().backgroundColor = purple;
                break;
        }
    }

    void Update() {
        if(gameControlScript != GameObject.Find("Level")) {
            if(GameObject.Find("Level") != null) {
                gameControlScript = GameObject.Find("Level").GetComponent<GameControl>();
            }
        }
        if(gameControlScript != null) {
            transform.position = gameControlScript.directionalLight.transform.position;
            transform.GetComponent<Camera>().orthographicSize = gameControlScript.orthographicSize;
        }
    }
}