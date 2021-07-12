using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class GameManager : MonoBehaviour {
    public PostProcessVolume volume;
    public enum gameStates {
        Idle, inGame, GameOver
    };
    private enum sugarRushStatus {
        Charging, Good, Bad
    };

    private sugarRushStatus rushStatus = sugarRushStatus.Charging;
    public gameStates gameState = gameStates.Idle;

    public float score;
    public int difficulity = 1;
    public static GameManager instance;
    public TextMeshProUGUI scoreUi;
    public GameObject player, gameOverUI, sugarRushBar;//Player's Object
    public float sugarRushValue = 50;

    void Start() {
        instance = this;
    }

    // Update is called once per frame
    void Update() {
        updateScore();

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

    private void updateScore() {
        string postFix = "";
        string _score = score.ToString();
        if (score > 1000f) {

            if ((score / 1000f) > 0 && (score / 1000f) < 999f) {
                _score = (score/1000f).ToString("F1");
                postFix = "K";
            } else if ((score / 1000000f) > 0) {
                _score = (score/1000000f).ToString("F2");
                postFix = "M";
            }
        }
        scoreUi.text = "Score: " + _score + postFix;
    }
    private void GameOver() {
        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        player.GetComponent<AudioSource>().PlayOneShot(player.GetComponent<PlayerController>().clips[2]);
        gameOverUI.SetActive(true);
        //Score checking
        //Achievement checking
    }

    public void AddToSugarRush(float increment) {
        if (rushStatus == sugarRushStatus.Charging) {
            sugarRushValue += increment;
            sugarRushBar.GetComponent<Slider>().value = Mathf.Clamp(sugarRushValue, 0, 100);
            if (sugarRushValue > 99) {
                rushStatus = sugarRushStatus.Good;
                StartCoroutine("SugarRushEffect");
                //Good
            } else if (sugarRushValue < 1) {
                rushStatus = sugarRushStatus.Bad;
                StartCoroutine("SugarRushEffect");
                //Bad
            }
        }
    }

    private IEnumerator SugarRushEffect() {
        if (rushStatus == sugarRushStatus.Good) {
            float currentJumpHeight = player.GetComponent<PlayerController>().jumpHeight;
            player.GetComponent<PlayerController>().jumpHeight = 7.5f;
            volume.profile.GetSetting<ChromaticAberration>().intensity.value = 1;
            while (sugarRushValue >= 25) {
                yield return new WaitForSeconds(0.2f);
                sugarRushValue--;
                sugarRushBar.GetComponent<Slider>().value = Mathf.Clamp(sugarRushValue, 0, 100);
            }
            volume.profile.GetSetting<ChromaticAberration>().intensity.value = 0;
            player.GetComponent<PlayerController>().jumpHeight = currentJumpHeight;

        } else {
            GameObject.FindGameObjectWithTag("MovementController").GetComponent<MovementController>().isInvert = 1;
            volume.profile.GetSetting<ColorGrading>().colorFilter.value = new Color(124f / 255f, 200f / 255f, 124f / 255f);
            while (sugarRushValue <= 50) {
                yield return new WaitForSeconds(0.2f);
                sugarRushValue++;
                sugarRushBar.GetComponent<Slider>().value = Mathf.Clamp(sugarRushValue, 0, 100);
            }
            GameObject.FindGameObjectWithTag("MovementController").GetComponent<MovementController>().isInvert = -1;
            volume.profile.GetSetting<ColorGrading>().colorFilter.value = new Color(1, 1, 1);
        }
        rushStatus = sugarRushStatus.Charging;
    }

}
