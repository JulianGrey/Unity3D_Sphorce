using UnityEngine;
using System.Collections;

public class VortexScript : MonoBehaviour {
    public AudioSource source;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Ball") {
            source.Play();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Ball") {
            source.Stop();
        }
    }

    void Update() {
        if(GameObject.Find("Current Ball") == null) {
            source.Stop();
        }
    }
}
