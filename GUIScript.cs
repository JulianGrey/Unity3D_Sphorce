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

    private bool howToPlay;
    private bool settings;

    public void ExitGame() {
        Application.Quit();
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

    public void HowToPlay() {
        howToPlay = true;
    }

    public void PauseGame() {
        gameControlScript.paused = true;
    }

    public void ResumeGame() {
        gameControlScript.paused = false;
    }

    public void ReturnToMenu() {
        if(settings) {
            settings = false;
        }
        else if(howToPlay) {
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
            Application.LoadLevel(gameControlScript.nextLevel);
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

        if(gameControlScript != null) {
            gameplayCanvas.enabled = true;
            mainMenuCanvas.enabled = false;
            if(gameControlScript.levelWin) {
                levelCompleteCanvas.enabled = true;
            }
            else {
                levelCompleteCanvas.enabled = false;
            }
            if(gameControlScript.paused) {
                pauseCanvas.enabled = true;
            }
            else {
                pauseCanvas.enabled = false;
            }
            if(gameControlScript.gameOver) {
                gameOverCanvas.enabled = true;
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
