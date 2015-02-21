using UnityEngine;
using System.Collections;

public class ActiveVisible : MonoBehaviour {
    
    void Update() {
        if(GameObject.Find("Actions").GetComponent<GameControl>().northActive) {
            if(gameObject.tag == "NorthActive") {
                var renderers = GetComponentsInChildren<Renderer>();
                foreach(var r in renderers) {
                    r.enabled = true;
                }
            }
            else {
                var renderers = GetComponentsInChildren<Renderer>();
                foreach(var r in renderers) {
                    r.enabled = false;
                }
            }
        }
        else if(GameObject.Find("Actions").GetComponent<GameControl>().eastActive) {
            if(gameObject.tag == "EastActive") {
                var renderers = GetComponentsInChildren<Renderer>();
                foreach(var r in renderers) {
                    r.enabled = true;
                }
            }
            else {
                var renderers = GetComponentsInChildren<Renderer>();
                foreach(var r in renderers) {
                    r.enabled = false;
                }
            }
        }
        else if(GameObject.Find("Actions").GetComponent<GameControl>().southActive) {
            if(gameObject.tag == "SouthActive") {
                var renderers = GetComponentsInChildren<Renderer>();
                foreach(var r in renderers) {
                    r.enabled = true;
                }
            }
            else {
                var renderers = GetComponentsInChildren<Renderer>();
                foreach(var r in renderers) {
                    r.enabled = false;
                }
            }
        }
        else if(GameObject.Find("Actions").GetComponent<GameControl>().westActive) {
            if(gameObject.tag == "WestActive") {
                var renderers = GetComponentsInChildren<Renderer>();
                foreach(var r in renderers) {
                    r.enabled = true;
                }
            }
            else {
                var renderers = GetComponentsInChildren<Renderer>();
                foreach(var r in renderers) {
                    r.enabled = false;
                }
            }
        }
        else {
            var renderers = GetComponentsInChildren<Renderer>();
            foreach(var r in renderers) {
                r.enabled = false;
            }
        }
    }
}
