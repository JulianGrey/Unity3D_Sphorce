using UnityEngine;
using System.Collections;

public class CanvasManager : MonoBehaviour {
    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
}
