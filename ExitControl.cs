using UnityEngine;
using System.Collections;

public class ExitControl : MonoBehaviour {
    
    void Update() {
        if(GameObject.Find("Actions").GetComponent<GameControl>().canFinish) {
            gameObject.renderer.enabled = false;
        }
        else {
            gameObject.renderer.enabled = true;
        }
    }
}
