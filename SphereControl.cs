using UnityEngine;
using System.Collections;

public class SphereControl : MonoBehaviour {
    private AudioManager audioScript;

    public GameObject ballDeathAnim;

    private float pushForce = 19.62f;
    
    private bool moveUp = false;
    private bool moveRight = false;
    private bool moveDown = false;
    private bool moveLeft = false;

    private bool ballExists;
    private bool inVortex;

    void Awake() {
        audioScript = GameObject.Find("AudioHandler").GetComponent<AudioManager>();
    }
    
    void Start() {
        GetComponent<AudioSource>().Stop();
        ballExists = true;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "LevelBlock") {
            if(ballExists) {
                AudioSource.PlayClipAtPoint(audioScript.gameSounds[2], new Vector2(0, 0));
                Instantiate(ballDeathAnim, gameObject.transform.position, Quaternion.identity);
                ballExists = false;
            }
            GameObject.Find("Level").GetComponent<GameControl>().respawn = true;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Finish") {
            if(GameObject.Find("Level").GetComponent<GameControl>().canFinish) {
                GameObject.Find("Level").GetComponent<GameControl>().levelWin = true;
                Destroy(gameObject);
            }
            else {
                //Debug.Log("Must retrieve objectives");
            }
        }
        if(collider.gameObject.tag == "Objective") {
            AudioSource.PlayClipAtPoint(audioScript.gameSounds[1], Camera.main.transform.position);
            GameObject.Find("Level").GetComponent<GameControl>().gotItems++;
            Destroy(collider.gameObject);
        }
        if(collider.gameObject.tag == "RightForce" || collider.gameObject.tag == "LeftForce" || collider.gameObject.tag == "UpForce" || collider.gameObject.tag == "DownForce") {
            if(collider.gameObject.tag == "RightForce") {
                moveRight = true;
            }
            else if(collider.gameObject.tag == "LeftForce") {
                moveLeft = true;
            }
            else if(collider.gameObject.tag == "UpForce") {
                moveUp = true;
            }
            else if(collider.gameObject.tag == "DownForce") {
                moveDown = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        if(collider.gameObject.tag == "RightForce" || collider.gameObject.tag == "LeftForce" || collider.gameObject.tag == "UpForce" || collider.gameObject.tag == "DownForce") {
            if(collider.gameObject.tag == "RightForce") {
                moveRight = false;
            }
            else if(collider.gameObject.tag == "LeftForce") {
                moveLeft = false;
            }
            else if(collider.gameObject.tag == "UpForce") {
                moveUp = false;
            }
            else if(collider.gameObject.tag == "DownForce") {
                moveDown = false;
            }
        }
    }

    void Update() {
        if(moveRight) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(pushForce, 0));
        }
        if(moveLeft) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(-pushForce, 0));
        }
        if(moveUp) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, pushForce));
        }
        if(moveDown) {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -pushForce));
        }
    }
}