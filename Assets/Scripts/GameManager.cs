using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    public enum gameStates {
        Idle, inGame, GameOver
    };

    public gameStates gameState = gameStates.Idle;

    public int score;
    public int difficulity = 1;
    public static GameManager instance;
    public TextMeshProUGUI scoreUi;
    public GameObject player;//Player's Object
    void Start() {
        instance = this;
    }

    // Update is called once per frame
    void Update() {
        scoreUi.text = "Score: " + score;

        //Difficulity Scaling based on score/current height\\
        if (score > 100) {
            difficulity = 5;
        } else if (score > 80) {
            difficulity = 4;
        } else if (score > 60) {
            difficulity = 3;
        } else if (score > 40) {
            difficulity = 2;
        }

        if (gameState == gameStates.Idle && (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)) {
            gameState = gameStates.inGame;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Rigidbody>().useGravity = true;
            player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0) * 5f, ForceMode.Impulse);

        } else if (gameState == gameStates.inGame && (Camera.main.transform.position.y - player.transform.position.y) > 2) {
            gameState = gameStates.GameOver;
            GameOver();
        }
    }

    private void GameOver() {
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        player.GetComponent<AudioSource>().PlayOneShot(player.GetComponent<PlayerController>().clips[2]);
        //Score checking
        //Achievement checking
    }
}
