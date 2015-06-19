using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

    public AudioManager audioScript;

    public GameObject ball;
    public GameObject objItem;
    private GameObject[] currentObjectives;

    public Transform ballSpawnPoint;
    public Transform[] objSpawnPoints;
    public GameObject directionalLight;

    public int livesLeft;
    public int gotItems = 0;
    private int levelItems = 0;

    private float nextRespawnTime = 2.0f;
    private float levelGravity = 9.81f * 2.5f;
    private float gyroscopeSensitivityModifier = 1.2f;
    public float orthographicSize;
    
    public string nextLevel;

    public bool respawn = false;
    public bool levelWin = false;
    public bool levelStart = true;
    public bool canFinish = false;
    public bool timing = false;
    private bool itemCount = false;
    private bool gameWin = false;
    public bool gameOver = false;
    public bool paused = false;
    private bool settings = false;
    public bool cameraFollowBall = false;

    void Start() {
        audioScript = GameObject.Find("AudioHandler").GetComponent<AudioManager>();

        itemCount = true;
        gameOver = false;
        gameWin = false;
        Time.timeScale = 1.0f;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    void AddBall() {
        Physics2D.gravity = new Vector2(0, 0);

        GameObject ballInstance = Instantiate(ball, ballSpawnPoint.position, Quaternion.identity) as GameObject;
        ballInstance.name = "Current Ball";
    }

    void AddObjectives() {
        if(itemCount == true) {
            for(int i = 0; i < objSpawnPoints.Length; i++) {
                Instantiate(objItem, objSpawnPoints[i].position, Quaternion.identity);
                levelItems++;
            }
            itemCount = false;
        }

        else if(itemCount == false) {
            for(int i = 0; i < objSpawnPoints.Length; i++) {
                Instantiate(objItem, objSpawnPoints[i].position, Quaternion.identity);
            }
        }
    }

    void LevelLoad() {
        if(levelWin) {
            if(!gameWin) {
                if(Input.GetKeyDown(KeyCode.Space) || Input.touchCount == 1) {
                    Application.LoadLevel(nextLevel);
                }
            }
            else {
                if(Input.GetKeyDown(KeyCode.Space) || Input.touchCount == 1) {
                    MainMenu();
                }
            }
        }
    }

    void LevelLose() {
        livesLeft = 0;

        if(!gameOver) {
            GetComponent<AudioSource>().PlayOneShot(audioScript.gameSounds[4]);
            gameOver = true;
        }
        if(Input.GetKeyDown(KeyCode.Space) || Input.touchCount == 1) {
            GameObject.Find("GUIHandler").GetComponent<GUIScript>().GoToMainMenu();
        }
        timing = false;
        LevelLoad();
    }

    public void LevelPause() {
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
            GetComponent<AudioSource>().PlayOneShot(audioScript.gameSounds[3]);

            timing = false;
        }

        if(nextLevel == "Frontend") {
            gameWin = true;
        }

        LevelLoad();
    }

    void MainMenu() {
        if(GameObject.Find("Timer") != null) {
            Destroy(GameObject.Find("Timer"));
        }
        if(GameObject.Find("Audio") != null) {
            Destroy(GameObject.Find("Audio"));
        }

        Application.LoadLevel("Frontend");
    }

    void PlaySelectSound() {
        GetComponent<AudioSource>().PlayOneShot(audioScript.gameSounds[0]);
    }

    void RemoveObjectives() {
        if(respawn) {
            currentObjectives = GameObject.FindGameObjectsWithTag("Objective");

            for(var i = 0; i < currentObjectives.Length; i++) {
                Destroy(currentObjectives[i]);
            }
        }
    }

    void Update() {
        if(!paused && !levelWin && !gameOver) {
            #region PCBinds
            //if(Input.GetKeyDown(KeyCode.RightArrow) || Input.acceleration.x > Input.acceleration.y) {
            //    Physics2D.gravity = new Vector2(levelGravity, 0);

            //    if(!timing) {
            //        timing = true;
            //    }
            //}
            //else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.acceleration.x < Input.acceleration.y) {
            //    Physics2D.gravity = new Vector2(-levelGravity, 0);

            //    if(!timing) {
            //        timing = true;
            //    }
            //}
            //else if(Input.GetKeyDown(KeyCode.UpArrow) || Input.acceleration.y > Input.acceleration.x) {
            //    Physics2D.gravity = new Vector2(0, levelGravity);

            //    if(!timing) {
            //        timing = true;
            //    }
            //}
            //else if(Input.GetKeyDown(KeyCode.DownArrow) || Input.acceleration.y < Input.acceleration.x) {
            //    Physics2D.gravity = new Vector2(0, -levelGravity);

            //    if(!timing) {
            //        timing = true;
            //    }
            //}
            #endregion
            Physics2D.gravity = new Vector2(levelGravity * (Input.acceleration.x * gyroscopeSensitivityModifier),
                                            levelGravity * (Input.acceleration.y * gyroscopeSensitivityModifier));

            if(!timing) {
                timing = true;
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

                // Add a delay to the respawning of the objectives and ball
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

        if(!levelWin && !gameOver) {
            if(Input.GetKeyDown(KeyCode.Escape)) {
                PlaySelectSound();
                LevelPause();

                if(settings) {
                    settings = false;
                }
            }
        }
    }
}
