using UnityEngine;
using System.Collections;

public class DelayDestroy : MonoBehaviour {
    
    void Update() {
        Invoke("RemoveObject", 1.0f);
    }
    
    void RemoveObject() {
        Destroy(gameObject);
    }
}
