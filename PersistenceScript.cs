using UnityEngine;
using System.Collections;

public class PersistenceScript : MonoBehaviour {

    void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
}
