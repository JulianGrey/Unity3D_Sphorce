using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {
    
    public AudioClip gameOverMusic;
    public AudioClip selectSound;
    public AudioClip winSound;

    public GameObject ball;
    public GameObject objItem;
    private GameObject[] currentObjectives;
        
    public Transform ballSpawnPoint;
    public Transform[] objSpawnPoint;
    public uint livesLeft;
    public uint gotItems = 0;
    private uint levelItems = 0;

    private float nextRespawnTime = 2.0f;
    private float levelGravity = 9.81f;
    
    public string nextLevel;

    public bool northActive = false;
    public bool eastActive = false;
    public bool southActive = false;
    public bool westActive = false;
    
    public bool respawn = false;
    public bool levelWin = false;
    private bool levelStart = true;
    public bool canFinish = false;
    public bool timing = false;
    private bool itemCount = false;
    private bool gameWin = false;
    private bool gameOver = false;
    private bool paused = false;
    private bool settings = false;

    public GUIStyle customHUDGUI;
    public GUIStyle customMenuGUI;
    public GUIStyle fadeGUI;
    public GUIStyle finishGUI;
    public GUIStyle titleGUI;
    public GUIStyle sliderGUI;

    void Start() {
        itemCount = true;
        gameOver = false;
        gameWin = false;
        Time.timeScale = 1.0f;
    }

    void OnGUI() {
        GUI.Box(new Rect(10, 10, 300, 60), "Lives remaining: " + livesLeft, customHUDGUI);

        if(GameObject.Find("Timer") != null) {
            GUI.Box(new Rect(Screen.width - 310, 10, 300, 60), "Timer: " + GameObject.Find("Timer").GetComponent<TimerScript>().truncTimer, customHUDGUI);
        }

        if(paused) {
            if(!settings) {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", fadeGUI);
                GUI.Box(new Rect((Screen.width / 2) - 70, (Screen.height / 4) - 60, 140, 100), "PAUSED", titleGUI);
                if(GUI.Button(new Rect((Screen.width / 2) - 102, (Screen.height / 2) - 60, 204, 60), "Resume Game", customMenuGUI)) { // Desktop: (Screen.height / 2) - 90 | Web: (Screen.height / 2) - 60
                    PlaySelectSound();
                    LevelPause();
                }
                if(GUI.Button(new Rect((Screen.width / 2) - 102, (Screen.height / 2), 204, 60), "Settings", customMenuGUI)) { // Desktop: (Screen.height / 2) - 30 | Web: (Screen.height / 2)
                    settings = true;
                }
                if(GUI.Button(new Rect((Screen.width / 2) - 102, (Screen.height / 2) + 60, 204, 60), "To Main Menu", customMenuGUI)) { // Desktop: (Screen.height / 2) + 30 | Web: (Screen.height / 2) + 60
                    LevelPause();
                    MainMenu();
                }
                /*if(GUI.Button(new Rect((Screen.width / 2) - 102, (Screen.height / 2) + 90, 204, 60), "Exit To Desktop", customMenuGUI)) {
                    Application.Quit();
                }*/
            }
        }

        if(settings) {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", fadeGUI);
            GUI.Box(new Rect((Screen.width / 2) - 150, (Screen.height / 2) - 95, 300, 160), "", sliderGUI);
            GUI.Box(new Rect((Screen.width / 2) - 50, (Screen.height / 2) - 50, 100, 30), "Volume", titleGUI);

            AudioListener.volume = GUI.HorizontalSlider(new Rect((Screen.width / 2) - 100, (Screen.height / 2), 200, 10), AudioListener.volume, 0.0f, 1.0f);
            if(GUI.Button(new Rect((Screen.width / 2) - 102, Screen.height - 100, 204, 60), "Back", customMenuGUI))
                settings = false;
        }

        if(levelWin) {
            if(!gameWin) {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", fadeGUI);
                GUI.Box(new Rect((Screen.width / 2) - 70, Screen.height / 4, 140, 100), "LEVEL COMPLETE", titleGUI);
                GUI.Box(new Rect((Screen.width / 2) - 150, (Screen.height / 2) + 50, 300, 100), "Press 'Space' to continue\nto the next level", finishGUI);
            }
            else {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", fadeGUI);
                GUI.Box(new Rect((Screen.width / 2) - 70, Screen.height / 4, 140, 100), "CONGRATULATIONS!!\nYOU WIN!!", titleGUI);
                GUI.Box(new Rect((Screen.width / 2) - 150, (Screen.height / 2) + 0, 300, 100), "Your time is: " + GameObject.Find("Timer").GetComponent<TimerScript>().truncTimer + " seconds" +
                        "\n\nThanks for playing!!", finishGUI);
                if(GUI.Button(new Rect((Screen.width / 2) - 102, (Screen.height / 2) + 130, 204, 60), "To Main Menu", customMenuGUI)) {
                    MainMenu();
                }
            }
        }
        if(gameOver) {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "", fadeGUI);
            GUI.Box(new Rect((Screen.width / 2) - 70, Screen.height / 4, 140, 100), "GAME OVER", titleGUI);
            Destroy(GameObject.Find("Audio"));
            if(GUI.Button(new Rect((Screen.width / 2) - 102, (Screen.height / 2) + 50, 204, 60), "To Main Menu", customMenuGUI)) {
                MainMenu();
            }
        }
    }

    void Update() {
        if(!paused && !levelWin && !gameOver) {
            if(Input.GetKeyDown(KeyCode.RightArrow)) {
                Physics2D.gravity = new Vector2(levelGravity, 0);

                northActive = false;
                eastActive = true;
                southActive = false;
                westActive = false;

                if(!timing) {
                    timing = true;
                }
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow)) {
                Physics2D.gravity = new Vector2(-levelGravity, 0);
                northActive = false;
                eastActive = false;
                southActive = false;
                westActive = true;
                if(!timing) {
                    timing = true;
                }
            }
            if(Input.GetKeyDown(KeyCode.UpArrow)) {
                Physics2D.gravity = new Vector2(0, levelGravity);
                northActive = true;
                eastActive = false;
                southActive = false;
                westActive = false;
                if(!timing) {
                    timing = true;
                }
            }
            if(Input.GetKeyDown(KeyCode.DownArrow)) {
                Physics2D.gravity = new Vector2(0, -levelGravity);
                northActive = false;
                eastActive = false;
                southActive = true;
                westActive = false;
                if(!timing) {
                    timing = true;
                }
            }
        }

        if(levelStart) {
            gameOver = false;
            AddBall();
            AddObjectives();
            livesLeft = 5;
            paused = false;
            levelStart = false;
        }

        if(respawn) {
            if(livesLeft > 1) {
                livesLeft--;
                gotItems = 0;
                RemoveObjectives();

                Invoke("AddObjectives", nextRespawnTime);
                Invoke("AddBall", nextRespawnTime);

                respawn = false;
            }
            else {
                LevelLose();
            }
        }

        if(gotItems == levelItems) {
            canFinish = true;
        }
        else {
            canFinish = false;
        }

        if(levelWin == true) {
            LevelWin();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && !levelWin && !gameOver) {
            PlaySelectSound();
            LevelPause();

            if(settings) {
                settings = false;
            }
        }
    }

    void MainMenu() {
        if(GameObject.Find ("Timer") != null) {
            Destroy(GameObject.Find("Timer"));
        }
        if(GameObject.Find ("Audio") != null) {
            Destroy(GameObject.Find("Audio"));
        }

        Application.LoadLevel("Frontend");
    }

    void LevelLoad()
    {
        if(levelWin) {
            if(!gameWin) {
                if(Input.GetKeyDown(KeyCode.Space)) {
                    Application.LoadLevel(nextLevel);
                }
            }
            else {
                if(Input.GetKeyDown(KeyCode.Space)) {
                    MainMenu();
                }
            }
        }
    }

    void AddBall() {
        Physics2D.gravity = new Vector2(0, 0);

        northActive = false;
        eastActive = false;
        southActive = false;
        westActive = false;

        GameObject ballInstance = Instantiate(ball, ballSpawnPoint.position, Quaternion.identity) as GameObject;
        ballInstance.name = "Current Ball";
    }

    void AddObjectives() {
        if(itemCount == true) {
            for(int i = 0; i < objSpawnPoint.Length; i++) {
                Instantiate(objItem, objSpawnPoint[i].position, Quaternion.identity);
                levelItems++;
            }
            itemCount = false;
        }

        else if(itemCount == false) {
            for(int i = 0; i < objSpawnPoint.Length; i++) {
                Instantiate(objItem, objSpawnPoint[i].position, Quaternion.identity);
            }
        }
    }

    void RemoveObjectives() {
        if(respawn) {
            currentObjectives = GameObject.FindGameObjectsWithTag("Objective");

            for(var i = 0; i < currentObjectives.Length; i++) {
                Destroy(currentObjectives[i]);
            }
        }
    }

    void PlaySelectSound() {
        audio.PlayOneShot(selectSound);
    }

    void LevelPause() {
        if(Time.timeScale == 0f) {
            Time.timeScale = 1.0f;
            paused = false;
        }
        else {
            Time.timeScale = 0f;
            paused = true;
        }
    }

    void LevelWin() {
        if(timing) {
            audio.PlayOneShot(winSound);

            timing = false;
        }

        if(nextLevel == "Frontend") {
            gameWin = true;
        }

        LevelLoad();
    }

    void LevelLose() {
        livesLeft = 0;

        if(!gameOver) {
            audio.PlayOneShot(gameOverMusic);
            gameOver = true;
        }
        timing = false;
        LevelLoad();
    }
}