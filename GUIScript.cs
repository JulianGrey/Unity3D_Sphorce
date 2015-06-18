using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GUIScript : MonoBehaviour {
    public GameControl gameControlScript;

    public Canvas mainMenuCanvas;
    public Canvas gameplayCanvas;
    public Canvas howToPlayCanvas;
    public Canvas pauseCanvas;

    private bool howToPlay;
    private bool settings;

    void Start() {
        gameControlScript = GameObject.Find("Actions").GetComponent<GameControl>();
    }

    public void StartGame() {
        mainMenuCanvas.enabled = false;
        gameplayCanvas.enabled = true;
        Application.LoadLevel("Level_01");
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
        Destroy(GameObject.Find("GUIHandler"));
        Destroy(GameObject.Find("TimerHandler"));
        Application.LoadLevel("Frontend");
    }

    public void ExitGame() {
        Application.Quit();
    }

    void Update() {
        if(howToPlay) {
            howToPlayCanvas.enabled = true;
        }
        else {
            howToPlayCanvas.enabled = false;
        }
        if(pauseCanvas.enabled) {
            gameplayCanvas.enabled = false;
        }
        else {
            gameplayCanvas.enabled = true;
        }
    }
}
