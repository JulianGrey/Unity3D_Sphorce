using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIScript : MonoBehaviour {
    private GameControl gameControlScript;

    public Canvas gameplayCanvas;
    public Canvas howToPlayCanvas;
    public Canvas mainMenuCanvas;
    public Canvas pauseCanvas;

    public string nextLevel;

    private bool howToPlay;
    private bool settings;

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
    }
}
