using UnityEngine;
using System.Collections;

// This script controls the visual aspects of the red bars on the level, indicating the direction of gravity. This script is
// attached to the sprites so they are controlled expicitly rather than handling them through the main script

public class ActiveVisible : MonoBehaviour
{
	
	// Update is called once per frame
	void Update()
	{
		// If the northActive variable in the main script is true, i.e. if gravity is moving in the Up direction
		if(GameObject.Find("Actions").GetComponent<GameControl>().northActive)
		{
			// If the object this script is attached to is tagged NorthActive, check its components and make them visible
			if(gameObject.tag == "NorthActive")
			{
				var renderers = GetComponentsInChildren<Renderer>();
				foreach(var r in renderers)
				{
					r.enabled = true;
				}
			}
			// Otherwise set their visibilities to false
			else
			{
				var renderers = GetComponentsInChildren<Renderer>();
				foreach(var r in renderers)
				{
					r.enabled = false;
				}
			}
		}
		else if(GameObject.Find("Actions").GetComponent<GameControl>().eastActive)
		{
			if(gameObject.tag == "EastActive")
			{
				var renderers = GetComponentsInChildren<Renderer>();
				foreach(var r in renderers)
				{
					r.enabled = true;
				}
			}
			else
			{
				var renderers = GetComponentsInChildren<Renderer>();
				foreach(var r in renderers)
				{
					r.enabled = false;
				}
			}
		}
		else if(GameObject.Find("Actions").GetComponent<GameControl>().southActive)
		{
			if(gameObject.tag == "SouthActive")
			{
				var renderers = GetComponentsInChildren<Renderer>();
				foreach(var r in renderers)
				{
					r.enabled = true;
				}
			}
			else
			{
				var renderers = GetComponentsInChildren<Renderer>();
				foreach(var r in renderers)
				{
					r.enabled = false;
				}
			}
		}
		else if(GameObject.Find("Actions").GetComponent<GameControl>().westActive)
		{
			if(gameObject.tag == "WestActive")
			{
				var renderers = GetComponentsInChildren<Renderer>();
				foreach(var r in renderers)
				{
					r.enabled = true;
				}
			}
			else
			{
				var renderers = GetComponentsInChildren<Renderer>();
				foreach(var r in renderers)
				{
					r.enabled = false;
				}
			}
		}
		else
		{
			var renderers = GetComponentsInChildren<Renderer>();
			foreach(var r in renderers)
			{
				r.enabled = false;
			}
		}
	}
}
