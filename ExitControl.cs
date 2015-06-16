using UnityEngine;
using System.Collections;

public class ExitControl : MonoBehaviour {
    
    void Update() {
        if(GameObject.Find("Actions").GetComponent<GameControl>().canFinish) {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
        else {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
    }
}
