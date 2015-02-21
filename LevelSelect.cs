using UnityEngine;
using System.Collections;

public class LevelSelect : MonoBehaviour {

    void OnGUI() {
        if(GUI.Button(new Rect((Screen.width/2) - 80,(Screen.height/2) - 80, 40, 40), "01")) {
            Application.LoadLevel("Level_01");
        }
        if(GUI.Button(new Rect((Screen.width/2) - 40,(Screen.height/2) - 80, 40, 40), "02")) {
            Application.LoadLevel("Level_02");
        }
        if(GUI.Button(new Rect((Screen.width/2),(Screen.height/2) - 80, 40, 40), "03")) {
            Application.LoadLevel("Level_03");
        }
        if(GUI.Button(new Rect((Screen.width/2) + 40,(Screen.height/2) - 80, 40, 40), "04")) {
            Application.LoadLevel("Level_04");
        }
        if(GUI.Button(new Rect((Screen.width/2) - 80,(Screen.height/2) - 40, 40, 40), "05")) {
            Application.LoadLevel("Level_05");
        }
        if(GUI.Button(new Rect((Screen.width/2) - 40,(Screen.height/2) - 40, 40, 40), "06")) {
            Application.LoadLevel("Level_06");
        }
        if(GUI.Button(new Rect((Screen.width/2),(Screen.height/2) - 40, 40, 40), "07")) {
            Application.LoadLevel("Level_07");
        }
        if(GUI.Button(new Rect((Screen.width/2) + 40,(Screen.height/2) - 40, 40, 40), "08")) {
            Application.LoadLevel("Level_08");
        }
        if(GUI.Button(new Rect((Screen.width/2) - 80,(Screen.height/2), 40, 40), "09")) {
            Application.LoadLevel("Level_09");
        }
        if(GUI.Button(new Rect((Screen.width/2) - 40,(Screen.height/2), 40, 40), "10")) {
            Application.LoadLevel("Level_10");
        }
        if(GUI.Button(new Rect((Screen.width/2),(Screen.height/2), 40, 40), "11")) {
            Application.LoadLevel("Level_11");
        }
        if(GUI.Button(new Rect((Screen.width/2) + 40,(Screen.height/2), 40, 40), "12")) {
            Application.LoadLevel("Level_12");
        }
        if(GUI.Button(new Rect((Screen.width/2) - 80,(Screen.height/2) + 40, 40, 40), "13")) {
            Application.LoadLevel("Level_13");
        }
        if(GUI.Button(new Rect((Screen.width/2) - 40,(Screen.height/2) + 40, 40, 40), "14")) {
            Application.LoadLevel("Level_14");
        }
        if(GUI.Button(new Rect((Screen.width/2),(Screen.height/2) + 40, 40, 40), "15")) {
            Application.LoadLevel("Level_15");
        }
        if(GUI.Button(new Rect((Screen.width/2) + 40,(Screen.height/2) + 40, 40, 40), "16")) {
            Application.LoadLevel("Level_16");
        }
        if(GUI.Button(new Rect((Screen.width / 2) - 70, Screen.height - 60, 140, 30), "Back to Main Menu")) {
            Application.LoadLevel("Frontend");
        }
    }
}
