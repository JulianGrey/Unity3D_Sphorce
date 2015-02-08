using UnityEngine;
using System.Collections;

public class Frontend : MonoBehaviour
{
	public GUIStyle customMenuGUI;
	public GUIStyle customDialogGUI;
	public GUIStyle fadeGUI;
	public GUIStyle titleGUI;
	public GUIStyle sliderGUI;
	public GUIStyle smallFontGUI;

	private bool howToPlay = false;
	private bool settings = false;

	void OnGUI()
	{
		if(!howToPlay && !settings)
		{
			if(GUI.Button(new Rect((Screen.width / 2) - 102, (Screen.height / 2) - 60, 204, 60), "Start Game", customMenuGUI))
				// Desktop: (Screen.height / 2) - 75
				// Web: (Screen.height / 2) - 60
			{
				Application.LoadLevel("Level_01");
			}
			if(GUI.Button(new Rect((Screen.width / 2) - 102, (Screen.height / 2), 204, 60), "Settings", customMenuGUI))
				// Desktop: (Screen.height / 2) - 15
				// Web: (Screen.height / 2)
			{
				settings = true;
			}
			if(GUI.Button (new Rect((Screen.width / 2) - 102, (Screen.height / 2) + 60, 204, 60), "How To Play", customMenuGUI))
				// Desktop: (Screen.height / 2) + 45
				// Web: (Screen.height / 2) + 60
			{
				howToPlay = true;
			}
			/*if(GUI.Button (new Rect((Screen.width / 2) - 102, (Screen.height / 2) + 105, 204, 60), "Quit Game", customMenuGUI))
			{
				Application.Quit();
			}*/
			GUI.Box (new Rect((Screen.width / 2) - 50, Screen.height - 40, 100, 20), "Created and developed by Julian Grey", smallFontGUI);
		}

		if(settings)
		{
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", fadeGUI);
			GUI.Box (new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 95, 300, 160), "", sliderGUI);
			GUI.Box (new Rect((Screen.width / 2) - 50, (Screen.height / 2) - 50, 100, 30), "Volume", customDialogGUI);
			AudioListener.volume = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 100, (Screen.height / 2), 200, 10), AudioListener.volume, 0.0f, 1.0f);

			if(GUI.Button(new Rect((Screen.width / 2) - 102, Screen.height - 100, 204, 60), "Back", customMenuGUI))
				settings = false;
		}

		if(howToPlay)
		{
			GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", fadeGUI);
			GUI.Box(new Rect((Screen.width / 2) - 100, Screen.height / 4, 200, 50), "How To Play", titleGUI);
			GUI.Box(new Rect((Screen.width / 2) - 200, (Screen.height / 2) - 175, 400, 300), "" +
			        "\n\n\n\nMove the ball by using the arrow keys\nto change the direction of gravity." + "\nCollect all the items" +
			        " in the level." + "\nComplete the level by reaching the exit.\n\nDO NOT TOUCH THE WALLS!", customDialogGUI);
			if(GUI.Button(new Rect((Screen.width / 2) - 102, Screen.height - 100, 204, 60), "Back", customMenuGUI))
				howToPlay = false;
		}
	}
}
