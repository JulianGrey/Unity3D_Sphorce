using UnityEngine;
using System.Collections;

public class ItemControl : MonoBehaviour {
    
    public float rotateSpeed = 20.0f;
    
    void Update() {
        transform.Rotate(new Vector3(0, 0, 1) * rotateSpeed * Time.deltaTime);
    }
}
