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
    void Start() {
        instance = this;
    }

    // Update is called once per frame
    void Update() {
        scoreUi.text = "Score: " + score;

        if (score > 100) {
            difficulity = 5;
        } else if (score > 80) {
            difficulity = 4;
        } else if (score > 60) {
            difficulity = 3;
        } else if (score > 40) {
            difficulity = 2;
        }

        if (gameState == gameStates.Idle && Input.GetKeyDown(KeyCode.Space)) {
            gameState = gameStates.inGame;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Rigidbody>().useGravity = true;
            player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0) * 5f, ForceMode.Impulse);
        }
    }
}
