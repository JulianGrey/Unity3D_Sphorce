using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIScript : MonoBehaviour {
    private GameControl gameControlScript;

    public Canvas[] canvases;

    private Canvas gameplayCanvas;
    private Canvas howToPlayCanvas;
    private Canvas mainMenuCanvas;
    private Canvas pauseCanvas;
    private Canvas levelCompleteCanvas;

    public string nextLevel;

    private bool howToPlay;
    private bool settings;

    void Awake() {
        gameplayCanvas = canvases[0];
        howToPlayCanvas = canvases[1];
        mainMenuCanvas = canvases[2];
        pauseCanvas = canvases[3];
        levelCompleteCanvas = canvases[4];
    }

    public void StartNextLevel() {
        if(mainMenuCanvas.enabled) {
            mainMenuCanvas.enabled = false;
        }
        if(!gameplayCanvas.enabled) {
            gameplayCanvas.enabled = true;
        }
        if(gameControlScript != null) {
            Application.LoadLevel(gameControlScript.nextLevel);
        }
        else {
            Application.LoadLevel("Level_01");
        }
    }

    public void PauseGame() {
        gameControlScript.LevelPause();
        pauseCanvas.enabled = true;
    }

    public void ResumeGame() {
        gameControlScript.LevelPause();
        pauseCanvas.enabled = false;
    }

    public void HowToPlay() {
        howToPlay = true;
    }

    public void ReturnToMenu() {
        if(settings) {
            settings = false;
        }
        else if(howToPlay) {
            howToPlay = false;
        }
    }

    public void GoToMainMenu() {
        Destroy(GameObject.Find("AudioHandler"));
        Destroy(GameObject.Find("CanvasHandler"));
        Destroy(GameObject.Find("EventSystem"));
        Destroy(GameObject.Find("GUIHandler"));
        Destroy(GameObject.Find("Main Camera"));
        Destroy(GameObject.Find("TimerHandler"));
        Application.LoadLevel("Frontend");
    }

    public void ExitGame() {
        Application.Quit();
    }

    void Update() {
        if(gameControlScript != GameObject.Find("Level")) {
            if(GameObject.Find("Level") != null) {
                gameControlScript = GameObject.Find("Level").GetComponent<GameControl>();
            }
        }

        if(howToPlay) {
            howToPlayCanvas.enabled = true;
        }
        else {
            howToPlayCanvas.enabled = false;
        }
        if(pauseCanvas.enabled || gameControlScript == null) {
            gameplayCanvas.enabled = false;
        }
        else {
            gameplayCanvas.enabled = true;
        }
        if(gameControlScript != null) {
            if(gameControlScript.levelWin) {
                levelCompleteCanvas.enabled = true;
            }
            else {
                levelCompleteCanvas.enabled = false;
            }
        }
    }
}
