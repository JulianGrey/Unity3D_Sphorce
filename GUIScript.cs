using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIScript : MonoBehaviour {
    private GameControl gameControlScript;

    public Canvas gameplayCanvas;
    public Canvas howToPlayCanvas;
    public Canvas mainMenuCanvas;
    public Canvas pauseCanvas;
    public Canvas levelCompleteCanvas;
    public Canvas gameOverCanvas;

    private Camera gameCamera;

    private bool howToPlay;

    void Awake() {
        gameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void GoToMainMenu() {
        Destroy(GameObject.Find("AudioHandler"));
        Destroy(GameObject.Find("CanvasHandler"));
        Destroy(GameObject.Find("EventSystem"));
        Destroy(GameObject.Find("GUIHandler"));
        Destroy(GameObject.Find("TimerHandler"));
        Application.LoadLevel("Frontend");
    }

    public void HowToPlay() {
        howToPlay = true;
    }

    public void PauseGame() {
        pauseCanvas.enabled = true;
        gameplayCanvas.enabled = false;
        gameControlScript.LevelPause();
    }

    public void ResumeGame() {
        pauseCanvas.enabled = false;
        gameplayCanvas.enabled = true;
        gameControlScript.LevelPause();
    }

    public void ReturnToMenu() {
        if(howToPlay) {
            howToPlay = false;
        }
    }

    public void StartNextLevel() {
        if(mainMenuCanvas.enabled) {
            mainMenuCanvas.enabled = false;
        }
        if(!gameplayCanvas.enabled) {
            gameplayCanvas.enabled = true;
        }
        if(gameControlScript != null) {
            if(gameControlScript.nextLevel == "Frontend") {
                GoToMainMenu();
            }
            else {
                Application.LoadLevel(gameControlScript.nextLevel);
            }
        }
        else {
            Application.LoadLevel("Level_01");
        }
    }

    void Update() {

        if(gameControlScript != GameObject.Find("Level")) {
            if(GameObject.Find("Level") != null) {
                gameControlScript = GameObject.Find("Level").GetComponent<GameControl>();
            }
        }

        if(gameCamera != null) {
            mainMenuCanvas.worldCamera = gameCamera;
            gameplayCanvas.worldCamera = gameCamera;
            howToPlayCanvas.worldCamera = gameCamera;
            pauseCanvas.worldCamera = gameCamera;
            levelCompleteCanvas.worldCamera = gameCamera;
            gameOverCanvas.worldCamera = gameCamera;
        }

        if(gameControlScript != null) {
            gameplayCanvas.enabled = true;
            mainMenuCanvas.enabled = false;
            if(gameControlScript.levelWin) {
                levelCompleteCanvas.enabled = true;
                gameplayCanvas.enabled = false;
                pauseCanvas.enabled = false;
            }
            else {
                levelCompleteCanvas.enabled = false;
            }
            if(gameControlScript.paused) {
                gameplayCanvas.enabled = false;
            }
            else {
                gameplayCanvas.enabled = true;
            }
            if(gameControlScript.gameOver) {
                gameOverCanvas.enabled = true;
                gameplayCanvas.enabled = false;
            }
            else {
                gameOverCanvas.enabled = false;
            }
        }
        else {
            gameplayCanvas.enabled = false;
            mainMenuCanvas.enabled = true;
            if(howToPlay) {
                howToPlayCanvas.enabled = true;
            }
            else {
                howToPlayCanvas.enabled = false;
            }
        }
    }
}
