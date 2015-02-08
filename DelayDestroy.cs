using UnityEngine;
using System.Collections;

// This script is exclusively used for the control of the ball's death sprite

public class DelayDestroy : MonoBehaviour
{
	// Update is called once per frame
	void Update()
	{
		Invoke("RemoveObject", 1.0f);
	}

	void RemoveObject()
	{
		Destroy(gameObject);
	}
}
