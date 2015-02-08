using UnityEngine;
using System.Collections;

// This script handles the exit object. The exit is made up of two components, an animated object and a
// "door". This script controls the visibility of the door, thus making the exit appear to be opened or closed.

public class ExitControl : MonoBehaviour
{
	
	// Update is called once per frame
	void Update()
	{
		// If the player can finish (this is when the number of objectives
		// collected equals the number of objectives initially spawned)
		if(GameObject.Find("Actions").GetComponent<GameControl>().canFinish)
		{
			gameObject.renderer.enabled = false; // Make the object invisible (open the door)
		}
		else
		{
			gameObject.renderer.enabled = true; // Make the object visible (close the door)
		}
	}
}
