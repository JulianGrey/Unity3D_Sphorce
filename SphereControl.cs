// #######################################################################################
// ## The purpose of this script is for handling the despawning of the ball object when ##
// ############ the player hits an object which results in the loss of a life ############
// #######################################################################################

using UnityEngine;
using System.Collections;

public class SphereControl : MonoBehaviour
{
	public AudioClip pickup;
	public AudioClip vortex;
	public AudioClip ballLost;

	public GameObject ballDeathAnim;

	private float pushForce = 19.62f;

	public bool northActive = false;
	public bool eastActive = false;
	public bool southActive = false;
	public bool westActive = false;

	// Boolean values handling trigger variables
	private bool moveRight = false;
	private bool moveLeft = false;
	private bool moveUp = false;
	private bool moveDown = false;

	private bool ballExists;

	void Start()
	{
		audio.Stop();
		ballExists = true;
	}

	// Set up collision detection for when the ball hits an object
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "LevelBlock")
		{
			if(ballExists)
			{
				AudioSource.PlayClipAtPoint(ballLost, new Vector2(0, 0));
				Instantiate(ballDeathAnim, gameObject.transform.position, Quaternion.identity);
				ballExists = false;
			}
			// change the "respawn" variable in the main script, and destroy the object
			GameObject.Find("Actions").GetComponent<GameControl>().respawn = true;
			Destroy(gameObject);
		}
	}

	// Set up 2D collision detection for when the ball hits a trigger
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Finish")
		{
			if(GameObject.Find("Actions").GetComponent<GameControl>().canFinish)
			{
				GameObject.Find("Actions").GetComponent<GameControl>().levelWin = true;
				Destroy(gameObject);
			}
			else
			{
				//Debug.Log("Must retrieve objectives");
			}
		}
		if(collider.gameObject.tag == "Objective")
		{
			//AudioSource.PlayClipAtPoint(pickup, GameObject.Find("Audio").transform.position);
			AudioSource.PlayClipAtPoint(pickup, Camera.main.transform.position);
			GameObject.Find("Actions").GetComponent<GameControl>().gotItems++;
			Destroy(collider.gameObject);
		}
		if(collider.gameObject.tag == "RightForce")
		{
			moveRight = true;
			audio.clip = vortex;
			audio.Play();
		}
		if(collider.gameObject.tag == "LeftForce")
		{
			moveLeft = true;
			audio.clip = vortex;
			audio.Play();
		}
		if(collider.gameObject.tag == "UpForce")
		{
			moveUp = true;
			audio.clip = vortex;
			audio.Play();
		}
		if(collider.gameObject.tag == "DownForce")
		{
			moveDown = true;
			audio.clip = vortex;
			audio.Play();
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "RightForce")
		{
			moveRight = false;
			audio.clip = vortex;
			audio.Stop();
		}
		if(collider.gameObject.tag == "LeftForce")
		{
			moveLeft = false;
			audio.clip = vortex;
			audio.Stop();
		}
		if(collider.gameObject.tag == "UpForce")
		{
			moveUp = false;
			audio.clip = vortex;
			audio.Stop();
		}
		if(collider.gameObject.tag == "DownForce")
		{
			moveDown = false;
			audio.clip = vortex;
			audio.Stop();
		}
	}

	void Update()
	{
		if(moveRight)
		{
			rigidbody2D.AddForce(new Vector2(pushForce, 0));
		}
		if(moveLeft)
		{
			rigidbody2D.AddForce(new Vector2(-pushForce, 0));
		}
		if(moveUp)
		{
			rigidbody2D.AddForce(new Vector2(0, pushForce));
		}
		if(moveDown)
		{
			rigidbody2D.AddForce(new Vector2(0, -pushForce));
		}
	}
}