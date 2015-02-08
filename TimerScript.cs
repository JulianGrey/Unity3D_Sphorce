using UnityEngine;
using System.Collections;

// This script is exclusive to the Timer object

public class TimerScript : MonoBehaviour
{
	public string truncTimer = "0.00"; // Truncated timer
	public float timer; // Raw timer

	// Use this for initialization
	void Awake()
	{
		DontDestroyOnLoad(transform.gameObject); // Prevent this object from getting destroyed on changing levels
	}
	
	// Update is called once per frame
	void Update ()
	{
		// When the main menu is loaded, set the timer and truncTimer values. This is more to control the variables
		// after a game over, rather than when the game is first started.
		if(Application.loadedLevelName == "Frontend")
		{
			timer = 0;
			truncTimer = "0.00";
		}

		// If the Actions object exists, and its timing variable is set to true, start the timer
		if(GameObject.Find ("Actions") != null)
		{
			if(GameObject.Find("Actions").GetComponent<GameControl>().timing)
			{
				timer += Time.deltaTime;
				truncTimer = timer.ToString("F2"); // Set the timer to two decimal places

				// This variable was an experimentation with making the timer set 60 seconds to a minute,
				// however I couldn't get it to work with the milliseconds, and therefore it was left out
				//truncTimer = string.Format("{0:0}:{1:00}", Mathf.Floor(timer/60), timer % 60);
			}
		}
	}
}
