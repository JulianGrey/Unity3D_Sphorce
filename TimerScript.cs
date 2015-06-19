using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour {
    
    public string truncTimer = "0.00";
    public float timer;
    
    void Update () {
        if(Application.loadedLevelName == "Frontend") {
            timer = 0;
            truncTimer = "0.00";
        }

        if(GameObject.Find ("Actions") != null) {
            if(GameObject.Find("Actions").GetComponent<GameControl>().timing) {
                timer += Time.deltaTime;
                truncTimer = timer.ToString("F2");
            }
        }
    }
}
