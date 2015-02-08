using UnityEngine;
using System.Collections;

// This script simply controls the rotation of the gameobject it's attached to

public class ItemControl : MonoBehaviour
{
	public float rotateSpeed = 20.0f;
	
	// Update is called once per frame
	void Update()
	{
		transform.Rotate(new Vector3(0, 0, 1) * rotateSpeed * Time.deltaTime); // Rotate in the Z-axis
	}
}
