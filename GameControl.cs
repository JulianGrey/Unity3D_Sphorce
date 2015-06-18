using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour {

    public AudioManager audioScript;

    public GameObject ball;
    public GameObject objItem;
    private GameObject[] currentObjectives;
        
    public Transform ballSpawnPoint;
    public Transform[] objSpawnPoint;
    public uint livesLeft;
    public uint gotItems = 0;
    private uint levelItems = 0;

    private float nextRespawnTime = 2.0f;
    private float levelGravity = 9.81f * 2.5f;
    private float gyroscopeSensitivityModifier = 1.2f;
    
    public string nextLevel;

    public bool respawn = false;
    public bool levelWin = false;
    private bool levelStart = true;
    public bool canFinish = false;
    public bool timing = false;
    private bool itemCount = false;
    private bool gameWin = false;
    private bool gameOver = false;
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

    void Update() {
        if(!paused && !levelWin && !gameOver) {
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

    void MainMenu() {
        if(GameObject.Find ("Timer") != null) {
            Destroy(GameObject.Find("Timer"));
        }
        if(GameObject.Find ("Audio") != null) {
            Destroy(GameObject.Find("Audio"));
        }

        Application.LoadLevel("Frontend");
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

    void AddBall() {
        Physics2D.gravity = new Vector2(0, 0);

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
        GetComponent<AudioSource>().PlayOneShot(audioScript.gameSounds[0]);
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

    void LevelLose() {
        livesLeft = 0;

        if(!gameOver) {
            GetComponent<AudioSource>().PlayOneShot(audioScript.gameSounds[4]);
            gameOver = true;
        }
        timing = false;
        LevelLoad();
    }
}