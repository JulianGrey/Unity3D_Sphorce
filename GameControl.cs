using UnityEngine;
using System.Collections;

// ###########################################################
// # This is the main script which handles the core gameplay #
// ###########################################################

public class GameControl : MonoBehaviour
{
	// Set up audio clips to be used ingame
	public AudioClip gameOverMusic;
	public AudioClip selectSound;
	public AudioClip winSound;

	public GameObject ball; // Variable holding the ball object
	public GameObject objItem; // Variable holding the objective object
	private GameObject[] currentObjectives; // Variable which will be used to find objects later
		
	public Transform ballSpawnPoint; // Set a variable holding the ball's spawn point
	public Transform[] objSpawnPoint; // Set a variable holding the objective's spawn point
	public uint livesLeft; // Number of lives
	public uint gotItems = 0; // Number of objectives collected
	private uint levelItems = 0; // Variable holding the initial number of objectives in the level

	// These timer variables are only used for debugging without the external timer object
	//private string truncTimer = "0.00"; // Truncated timer
	//private float timer; // Raw timer

	private float nextRespawnTime = 2.0f; // Wait timer for respawning gameobjects
	private float levelGravity = 9.81f; // Value held for gravity
	//private float deadZone = 0.2f; // Left stick deadzone

	public string nextLevel; // Variable holding the next level for loading on level completion

	// Boolean variables held for changing visuals on direction pressed
	public bool northActive = false;
	public bool eastActive = false;
	public bool southActive = false;
	public bool westActive = false;
	
	public bool respawn = false;
	public bool levelWin = false;
	private bool levelStart = true;
	public bool canFinish = false;
	public bool timing = false;
	private bool itemCount = false; // Boolean variable for counting the number of objectives on level start
	private bool gameWin = false;
	private bool gameOver = false;
	private bool paused = false;
	private bool settings = false;

	// various GUI styles used ingame
	public GUIStyle customHUDGUI;
	public GUIStyle customMenuGUI;
	public GUIStyle fadeGUI;
	public GUIStyle finishGUI;
	public GUIStyle titleGUI;
	public GUIStyle sliderGUI;

	// Use this for initialization
	void Start()
	{
		itemCount = true; // Allow the number of objectives to be counted on level load
		gameOver = false;
		gameWin = false;
		Time.timeScale = 1.0f; // Force the game to play at normal speed
	}

	void OnGUI()
	{
		GUI.Box(new Rect(10, 10, 300, 60), "Lives remaining: " + livesLeft, customHUDGUI); // Shows the number of lives left

		// Show the timer content from the external Timer object
		if(GameObject.Find("Timer") != null)
		{
			GUI.Box(new Rect(Screen.width - 310, 10, 300, 60), "Timer: " + GameObject.Find("Timer").GetComponent<TimerScript>().truncTimer, customHUDGUI);
		}

		if(paused) // If the game is paused
		{
			if(!settings) // If the settings panel isn't open
			{
				GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", fadeGUI);
				GUI.Box(new Rect((Screen.width / 2) - 70, (Screen.height / 4) - 60, 140, 100), "PAUSED", titleGUI);
				if(GUI.Button(new Rect((Screen.width / 2) - 102, (Screen.height / 2) - 60, 204, 60), "Resume Game", customMenuGUI))
					// Desktop: (Screen.height / 2) - 90
					// Web: (Screen.height / 2) - 60
				{
					PlaySelectSound(); // Run the PlaySelectSound function
					LevelPause(); // Run the LevelPause function
				}
				if(GUI.Button(new Rect((Screen.width / 2) - 102, (Screen.height / 2), 204, 60), "Settings", customMenuGUI))
					// Desktop: (Screen.height / 2) - 30
					// Web: (Screen.height / 2)
				{
					settings = true; // Set this variable to true, this will enable and disable various GUI elements
				}
				if(GUI.Button(new Rect((Screen.width / 2) - 102, (Screen.height / 2) + 60, 204, 60), "To Main Menu", customMenuGUI))
					// Desktop: (Screen.height / 2) + 30
					// Web: (Screen.height / 2) + 60
				{
					LevelPause(); // Unpause the game (this is necessary otherwise playing a new game will start paused
					MainMenu(); // Run the MainMenu function
				}
				/*if(GUI.Button(new Rect((Screen.width / 2) - 102, (Screen.height / 2) + 90, 204, 60), "Exit To Desktop", customMenuGUI))
					Application.Quit(); // Quit the application*/
			}
		}

		if(settings)
		{
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", fadeGUI);
			GUI.Box (new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 95, 300, 160), "", sliderGUI);
			GUI.Box (new Rect((Screen.width / 2) - 50, (Screen.height / 2) - 50, 100, 30), "Volume", titleGUI);

			AudioListener.volume = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 100, (Screen.height / 2), 200, 10), AudioListener.volume, 0.0f, 1.0f); // Create a slider to control the game's volume
			if(GUI.Button(new Rect((Screen.width / 2) - 102, Screen.height - 100, 204, 60), "Back", customMenuGUI))
				settings = false; // Disable the settings panel and return to the pause menu
		}

		if(levelWin) // If we've completed the level
		{
			if(!gameWin) // ... but the game isn't over
			{
				GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", fadeGUI);
				GUI.Box (new Rect((Screen.width / 2) - 70, Screen.height / 4, 140, 100), "LEVEL COMPLETE", titleGUI);
				GUI.Box(new Rect((Screen.width / 2) - 150, (Screen.height / 2) + 50, 300, 100), "Press 'Space' to continue\nto the next level", finishGUI);
			}
			else // If we've completed the game
			{
				GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", fadeGUI);
				GUI.Box (new Rect((Screen.width / 2) - 70, Screen.height / 4, 140, 100), "CONGRATULATIONS!!\nYOU WIN!!", titleGUI);
				GUI.Box(new Rect((Screen.width / 2) - 150, (Screen.height / 2) + 0, 300, 100), "Your time is: " + GameObject.Find("Timer").GetComponent<TimerScript>().truncTimer + " seconds" +
					"\n\nThanks for playing!!", finishGUI); // Return the current (completion) value of the Timer object, and display it as part of a string
				if(GUI.Button(new Rect((Screen.width / 2) - 102, (Screen.height / 2) + 130, 204, 60), "To Main Menu", customMenuGUI))
				{
					MainMenu();
				}
			}
		}
		if(gameOver) // If we ran out of lives
		{
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", fadeGUI);
			GUI.Box (new Rect((Screen.width / 2) - 70, Screen.height / 4, 140, 100), "GAME OVER", titleGUI);
			Destroy (GameObject.Find("Audio"));
			if(GUI.Button(new Rect((Screen.width / 2) - 102, (Screen.height / 2) + 50, 204, 60), "To Main Menu", customMenuGUI))
			{
				MainMenu();
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		// These values were created at the start of development as the game was initially going to be created to support
		// controller input. With these values, it was possible to account for the controller's dead zone (as Unity's
		// built-in dead zone handlers are s**t). However as development continued and it was clear that there would be
		// no need for the majority of the inputs available on a controller, I decided to make the game only receive
		// keyboard and mouse inputs. However these variables are being left in should I decide to include controller
		// support in the future.
		//float horizontalValue = Input.GetAxis("Horizontal");
		//float verticalValue = Input.GetAxis("Vertical");

		if(!paused && !levelWin && !gameOver) // If the game is not paused, and no completion conditions have taken place
		{
			// Listen for analog inputs
			if(/*horizontalValue > deadZone || */Input.GetKeyDown(KeyCode.RightArrow)) // Right input
			{
				Physics2D.gravity = new Vector2(levelGravity, 0); // Set the gravity direction towards the right of the screen

				// These boolean values are necessary to control the visual aspects of the level (the red bars on the level)
				northActive = false;
				eastActive = true;
				southActive = false;
				westActive = false;

				// Start the timer if it isn't already running (this variable is only used for initially moving at the start of a level)
				if(!timing)
					timing = true;
			}
			if(/*horizontalValue < -deadZone || */Input.GetKeyDown(KeyCode.LeftArrow)) // Left input
			{
				Physics2D.gravity = new Vector2(-levelGravity, 0);
				northActive = false;
				eastActive = false;
				southActive = false;
				westActive = true;
				if(!timing)
					timing = true;
			}
			if(/*verticalValue > deadZone || */Input.GetKeyDown(KeyCode.UpArrow)) // Up input
			{
				Physics2D.gravity = new Vector2(0, levelGravity);
				northActive = true;
				eastActive = false;
				southActive = false;
				westActive = false;
				if(!timing)
					timing = true;
			}
			if(/*verticalValue < -deadZone || */Input.GetKeyDown(KeyCode.DownArrow)) // Down input
			{
				Physics2D.gravity = new Vector2(0, -levelGravity);
				northActive = false;
				eastActive = false;
				southActive = true;
				westActive = false;
				if(!timing)
					timing = true;
			}
		}

		// Debugging variables
		/*if(timing)
		{
			timer += Time.deltaTime;
			truncTimer = timer.ToString("F2");
		}*/

		// Initiate the game
		if(levelStart == true)
		{
			gameOver = false; // Set the gameOver variable to be false (if for any reason it isn't)
			AddBall(); // Run the AddBall function
			AddObjectives(); // Run the AddObjectives function
			livesLeft = 5; // Set the number of lives to 5
			paused = false; // Force the game to be unpaused (if for any reason it is)
			levelStart = false; // Disable the start game function
		}

		// Lives system
		if(respawn == true) // This variable is set to true by the ball object on collding with a wall
		{
			// If we have lives remaining
			if(livesLeft > 1)
			{
				livesLeft--; // Decrement the number of lives
				gotItems = 0; // Reset the number of objective items picked up
				RemoveObjectives(); // Run the RemoveObjectives function

				// Set delays for when respective gameobjects can be respawned
				Invoke("AddObjectives", nextRespawnTime);
				Invoke("AddBall", nextRespawnTime);

				respawn = false; // Disable this code segment
				////////// THE DESTRUCTION OF THE BALL IS HANDLED IN AN EXTERNAL SCRIPT //////////
			}
			else
			{
				// The RemoveObjectives function here was intended to remove all objective items on losing the game,
				// however this resulted in losing a life when starting a new game, so the level is left as is
				// when the game is over
				//RemoveObjectives();

				LevelLose(); // Run the LevelLose function
			}
		}

		if(gotItems == levelItems) // If the player has collected all the objective items in the level
			canFinish = true;
		else
			canFinish = false;

		if(levelWin == true)
		{
			LevelWin(); // Run the LevelWin function
		}

		// If the player presses Escape while the game is playing and no completion conditions have been initiated
		if(Input.GetKeyDown(KeyCode.Escape) && !levelWin && !gameOver)
		{
			PlaySelectSound();
			LevelPause();

			if(settings)
			{
				// This variable is here to disable the settings panel when Escape is pressed, effectively
				// closing the settings screen AND pause menu in one go, returning to the game
				settings = false;
			}
		}
	}

	void MainMenu()
	{
		// If these gameobjects exist, destroy them (this is so we don't have multiple instances on returning to the main menu)
		if(GameObject.Find ("Timer") != null)
			Destroy(GameObject.Find("Timer"));
		if(GameObject.Find ("Audio") != null)
			Destroy(GameObject.Find("Audio"));

		Application.LoadLevel("Frontend"); // Return to the main menu
	}

	void LevelLoad()
	{
		if(levelWin) // If we completed the level
		{
			if(!gameWin) // ...but not the game
			{
				if(Input.GetKeyDown(KeyCode.Space))
					Application.LoadLevel(nextLevel);
			}
			else // If we've completed the game
			{
				if(Input.GetKeyDown(KeyCode.Space))
					MainMenu();
			}
		}
	}

	void AddBall() // Function handling the spawning of the ball
	{
		Physics2D.gravity = new Vector2(0, 0); // Reset the direction of gravity

		// Set all these variables to false (this will hide all the red bars on the level)
		northActive = false;
		eastActive = false;
		southActive = false;
		westActive = false;

		// Create an instance of the ball object, giving it a position at the level's spawn point, and give the object a name
		GameObject ballInstance = Instantiate(ball, ballSpawnPoint.position, Quaternion.identity) as GameObject;
		ballInstance.name = "Current Ball";
	}

	void AddObjectives() // Function handling the spawning of objectives
	{
		// This first code block is for level loading. When the level loads, it instantiates objective items
		// on their respective spawners, and then adds them to the levelItems value. This value is necessary
		// for detecting whether the player has collected all the objectives or not
		if(itemCount == true)
		{
			for(int i = 0; i < objSpawnPoint.Length; i++)
			{
				Instantiate(objItem, objSpawnPoint[i].position, Quaternion.identity);
				levelItems++;
			}
			itemCount = false;
		}

		// This second code block is for losing a life. When the player loses a life, the game removes all
		// the objective items in the level (this is handled in the next function). This code block
		// adds the objective items on their respective spawners, but does not alter the value of the
		// levelItems variable
		else if(itemCount == false)
		{
			for(int i = 0; i < objSpawnPoint.Length; i++) // If a spawn object exists
			{
				Instantiate(objItem, objSpawnPoint[i].position, Quaternion.identity); // Add an objective item at its location
			}
		}
	}

	void RemoveObjectives() // Function handling objective garbage collection
	{
		if(respawn)
		{
			currentObjectives = GameObject.FindGameObjectsWithTag("Objective"); // Look for all objective items

			for(var i = 0; i < currentObjectives.Length; i++) // If an objective exists
				Destroy(currentObjectives[i]); // Remove it
		}
	}

	void PlaySelectSound()
	{
		audio.PlayOneShot(selectSound);
	}

	void LevelPause()
	{
		if(Time.timeScale == 0f) // If paused
		{
			Time.timeScale = 1.0f;
			paused = false;
		}
		else // If not paused
		{
			Time.timeScale = 0f;
			paused = true;
		}
	}

	void LevelWin() // Function handling level win condition
	{
		// This variable looks at whether the timer is running, however it is being used
		// to tell whether the game is playing or not
		if(timing)
		{
			audio.PlayOneShot(winSound);

			// This boolean is used to prevent the winSound from looping. When the timer stops, the level has
			// either been won or lost, so I am taking advantage of this variable to handle the execution
			// and control of the above sound
			timing = false;
		}

		// This bit of code is for handling the last level. By setting the last level's next level to the main menu,
		// I can use its nextLevel variable to execute a separate set of instructions when the level is completed
		// compared to all the other levels that continue into a consecutive level
		if(nextLevel == "Frontend")
			gameWin = true;

		LevelLoad();
	}

	void LevelLose() // Function handling level lose condition
	{
		livesLeft = 0; // Force the number of lives to be 0, just to be sure

		// This variable acts like the timing variable in the LevelWin function in regards to executing
		// and controlling the playing of a sound without it looping unexpectedly
		if(!gameOver)
		{
			audio.PlayOneShot(gameOverMusic);
			gameOver = true; // Enable the game over function, so this code block cannot be executed
		}
		timing = false; // Stop the timer
		LevelLoad();
	}
}