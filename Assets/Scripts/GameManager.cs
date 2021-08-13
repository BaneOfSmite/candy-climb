using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public PostProcessVolume volume;
    public enum gameStates {
        Idle,
        inGame,
        GameOver
    }
    public enum sugarRushStatus {
        Charging,
        Good,
        Bad
    }

    public sugarRushStatus rushStatus = sugarRushStatus.Charging;
    public gameStates gameState = gameStates.Idle;

    public float score;
    public int difficulity = 1;
    public static GameManager instance;
    public TextMeshProUGUI scoreUi, gameOverCurrent, gameOverBest;
    public GameObject player, gameOverUI, sugarRushBar, newBestScoreUI; 
    public float sugarRushValue = 50;
    public ParticleSystem[] _particleSystems;
    public GameObject bullet, rotationMovement;
    public bool isPaused = false;

    void Start() {
        instance = this;
    }

    // Update is called once per frame
    void Update() {
        updateScore();
        if (Input.GetMouseButtonDown(0)) { //Get when player click/Tapped
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Enemy"))) { //Ray cast check if player clicked or tapped on an enemy
                GameObject _bullet = Instantiate(bullet, player.transform.position, Quaternion.identity); //Spawn bullet
                _bullet.GetComponent<Bullet>().target = hit.transform.gameObject; //Set target of bullet
                _bullet.transform.SetParent(rotationMovement.transform); //set the bullet to be a child of the rotational controller object
            }
        }

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

        //Start the game from Idle state\\
        if (gameState == gameStates.Idle && (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 0)) {
            gameState = gameStates.inGame;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Rigidbody>().useGravity = true;
            player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0) * 5f, ForceMode.Impulse); //First Jump

        } else if (gameState == gameStates.inGame && (Camera.main.transform.position.y - player.transform.position.y) > 2) {
            GameOver();
        }
    }

    private void updateScore() { //Update the score UI
        scoreUi.text = "Score: " + scoreFormatter(score);
        gameOverCurrent.text = "Current Score: " + scoreFormatter(score);
    }
    public void GameOver() {
        //Manage GameOver\\
        gameState = gameStates.GameOver;
        detectAchievement();
        if (score > AchievementManager.instance.toSave.getScore()) {
            gameOverBest.text = "Best Score: " + scoreFormatter(score);
            newBestScoreUI.SetActive(true);
            AchievementManager.instance.toSave.setScore(score);
        } else {
            gameOverBest.text = "Best Score: " + scoreFormatter(AchievementManager.instance.toSave.getScore());
        }

        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        player.GetComponent<AudioSource>().PlayOneShot(player.GetComponent<PlayerController>().clips[2]);
        gameOverUI.SetActive(true);
        AchievementManager.instance.SaveData(); //Save the game data
    }
    public void AddToSugarRush(float increment) {
        if (rushStatus == sugarRushStatus.Charging) {
            sugarRushValue += increment;
            sugarRushBar.GetComponent<Slider>().value = Mathf.Clamp(sugarRushValue, 0, 100); //Clamp the value to be between 0 and 100 for the slider.
            //Good\\
            if (sugarRushValue > 99) {
                rushStatus = sugarRushStatus.Good;
                StartCoroutine("SugarRushEffect");
            //Bad\\
            } else if (sugarRushValue < 1) {
                rushStatus = sugarRushStatus.Bad;
                StartCoroutine("SugarRushEffect");
            }
        }
    }
    private IEnumerator SugarRushEffect() {
        foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy")) { //Delete all enemies
            Destroy(e);
        }
        if (rushStatus == sugarRushStatus.Good) { //Run SugarRush
            Camera.main.GetComponent<AudioSource>().pitch = 1.5f;
            AchievementManager.instance.toSave.setSugarRush(AchievementManager.instance.toSave.getSugarRush() + 1); //Increase achievement counter
            //_particleSystems[0].Stop();
            float currentJumpHeight = player.GetComponent<PlayerController>().jumpHeight;
            player.GetComponent<PlayerController>().jumpHeight = 7.5f; //Increase jumpHeight
            volume.profile.GetSetting<ChromaticAberration>().intensity.value = 1; //Set the postProcessing Camera Effect
            while (sugarRushValue >= 25) { //Gradually reduce the bar over time
                yield return new WaitForSecondsRealtime(0.2f);
                sugarRushValue--;
                sugarRushBar.GetComponent<Slider>().value = Mathf.Clamp(sugarRushValue, 0, 100);
            }
            //Reset to base values
            volume.profile.GetSetting<ChromaticAberration>().intensity.value = 0;
            player.GetComponent<PlayerController>().jumpHeight = currentJumpHeight;
            //_particleSystems[0].Stop();

        } else { //Bad
            AchievementManager.instance.toSave.setHealthyVomit(AchievementManager.instance.toSave.getHealthyVomit() + 1); //Increase achievement counter
            _particleSystems[1].Play();
            Camera.main.GetComponent<AudioSource>().pitch = 0.5f;
            GameObject.FindGameObjectWithTag("MovementController").GetComponent<MovementController>().isInvert = 1; //Invert the controls
            volume.profile.GetSetting<ColorGrading>().colorFilter.value = new Color(124f / 255f, 200f / 255f, 124f / 255f); //Set the color filter to be greenish
            while (sugarRushValue <= 50) { //Gradually increase the bar over time
                yield return new WaitForSecondsRealtime(0.2f);
                sugarRushValue++;
                sugarRushBar.GetComponent<Slider>().value = Mathf.Clamp(sugarRushValue, 0, 100);
            }
            //Reset to base controls
            GameObject.FindGameObjectWithTag("MovementController").GetComponent<MovementController>().isInvert = -1;
            volume.profile.GetSetting<ColorGrading>().colorFilter.value = new Color(1, 1, 1);
            _particleSystems[1].Stop();
        }
        rushStatus = sugarRushStatus.Charging;
        Camera.main.GetComponent<AudioSource>().pitch = 1f;
    }
    public string scoreFormatter(float _score) { //Score Formatting Manager, Converts to 1000 to 1.00K and so on.
        string postFix = "";
        if (score > 1000f) {

            if ((_score / 1000f) > 0 && (_score / 1000f) < 999f) {
                postFix = (_score / 1000f).ToString("F1");
                postFix += "K";
            } else if ((_score / 1000000f) > 0) {
                postFix = (_score / 1000000f).ToString("F2");
                postFix += "M";
            }
        } else {
            postFix = _score.ToString();
        }
        return postFix;
    }

    private void detectAchievement() { //Detect most of the achievement status and update them, basically manages most of the achievement unlocks.
        //Reach Height Of X\\
        if (score >= 1000000) {
            if (AchievementManager.instance.toSave.getAchievements()[0] != 7) {
                AchievementManager.instance.toSave.getAchievements()[0] = 7;
            }
        } else if (score >= 100000) {
            if (AchievementManager.instance.toSave.getAchievements()[0] < 6) {
                AchievementManager.instance.toSave.getAchievements()[0] = 6;
            }
        } else if (score >= 10000) {
            if (AchievementManager.instance.toSave.getAchievements()[0] < 5) {
                AchievementManager.instance.toSave.getAchievements()[0] = 5;
            }
        } else if (score >= 1000) {
            if (AchievementManager.instance.toSave.getAchievements()[0] < 4) {
                AchievementManager.instance.toSave.getAchievements()[0] = 4;
            }
        } else if (score >= 500) {
            if (AchievementManager.instance.toSave.getAchievements()[0] < 3) {
                AchievementManager.instance.toSave.getAchievements()[0] = 3;
            }
        } else if (score >= 100) {
            if (AchievementManager.instance.toSave.getAchievements()[0] < 2) {
                AchievementManager.instance.toSave.getAchievements()[0] = 2;
            }
        } else if (score >= 50) {
            if (AchievementManager.instance.toSave.getAchievements()[0] < 1) {
                AchievementManager.instance.toSave.getAchievements()[0] = 1;
            }
        }
        //Sugar Rush X Times\\
        if (AchievementManager.instance.toSave.getSugarRush() > 100) {
            if (AchievementManager.instance.toSave.getAchievements()[1] != 6) {
                AchievementManager.instance.toSave.getAchievements()[1] = 6;
            }
        } else if (AchievementManager.instance.toSave.getSugarRush() >= 50) {
            if (AchievementManager.instance.toSave.getAchievements()[1] < 5) {
                AchievementManager.instance.toSave.getAchievements()[1] = 5;
            }
        } else if (AchievementManager.instance.toSave.getSugarRush() >= 25) {
            if (AchievementManager.instance.toSave.getAchievements()[1] < 4) {
                AchievementManager.instance.toSave.getAchievements()[1] = 4;
            }
        } else if (AchievementManager.instance.toSave.getSugarRush() >= 10) {
            if (AchievementManager.instance.toSave.getAchievements()[1] < 3) {
                AchievementManager.instance.toSave.getAchievements()[1] = 3;
            }
        } else if (AchievementManager.instance.toSave.getSugarRush() >= 5) {
            if (AchievementManager.instance.toSave.getAchievements()[1] < 2) {
                AchievementManager.instance.toSave.getAchievements()[1] = 2;
            }
        } else if (AchievementManager.instance.toSave.getSugarRush() >= 1) {
            if (AchievementManager.instance.toSave.getAchievements()[1] < 1) {
                AchievementManager.instance.toSave.getAchievements()[1] = 1;
            }
        }
        //Collected Collectable X Times\\
        if (AchievementManager.instance.toSave.getTotalCollected() >= 500) {
            if (AchievementManager.instance.toSave.getAchievements()[2] != 5) {
                AchievementManager.instance.toSave.getAchievements()[2] = 5;
            }
        } else if (AchievementManager.instance.toSave.getTotalCollected() >= 100) {
            if (AchievementManager.instance.toSave.getAchievements()[2] < 4) {
                AchievementManager.instance.toSave.getAchievements()[2] = 4;
            }
        } else if (AchievementManager.instance.toSave.getTotalCollected() >= 50) {
            if (AchievementManager.instance.toSave.getAchievements()[2] < 3) {
                AchievementManager.instance.toSave.getAchievements()[2] = 3;
            }
        } else if (AchievementManager.instance.toSave.getTotalCollected() >= 25) {
            if (AchievementManager.instance.toSave.getAchievements()[2] < 2) {
                AchievementManager.instance.toSave.getAchievements()[2] = 2;
            }
        } else if (AchievementManager.instance.toSave.getTotalCollected() >= 10) {
            if (AchievementManager.instance.toSave.getAchievements()[2] < 1) {
                AchievementManager.instance.toSave.getAchievements()[2] = 1;
            }
        }
        //Vomit X Times\\
        if (AchievementManager.instance.toSave.getHealthyVomit() > 100) {
            if (AchievementManager.instance.toSave.getAchievements()[3] != 6) {
                AchievementManager.instance.toSave.getAchievements()[3] = 6;
            }
        } else if (AchievementManager.instance.toSave.getHealthyVomit() >= 50) {
            if (AchievementManager.instance.toSave.getAchievements()[3] < 5) {
                AchievementManager.instance.toSave.getAchievements()[3] = 5;
            }
        } else if (AchievementManager.instance.toSave.getHealthyVomit() >= 25) {
            if (AchievementManager.instance.toSave.getAchievements()[3] < 4) {
                AchievementManager.instance.toSave.getAchievements()[3] = 4;
            }
        } else if (AchievementManager.instance.toSave.getHealthyVomit() >= 10) {
            if (AchievementManager.instance.toSave.getAchievements()[3] < 3) {
                AchievementManager.instance.toSave.getAchievements()[3] = 3;
            }
        } else if (AchievementManager.instance.toSave.getHealthyVomit() >= 5) {
            if (AchievementManager.instance.toSave.getAchievements()[3] < 2) {
                AchievementManager.instance.toSave.getAchievements()[3] = 2;
            }
        } else if (AchievementManager.instance.toSave.getHealthyVomit() >= 1) {
            if (AchievementManager.instance.toSave.getAchievements()[3] < 1) {
                AchievementManager.instance.toSave.getAchievements()[3] = 1;
            }
        }
        //Killed X Fruit Guards\\
        if (AchievementManager.instance.toSave.getEnemiesKilled() >= 50) {
            if (AchievementManager.instance.toSave.getAchievements()[5] != 5) {
                AchievementManager.instance.toSave.getAchievements()[5] = 5;
            }
        } else if (AchievementManager.instance.toSave.getEnemiesKilled() >= 25) {
            if (AchievementManager.instance.toSave.getAchievements()[5] < 4) {
                AchievementManager.instance.toSave.getAchievements()[5] = 4;
            }
        } else if (AchievementManager.instance.toSave.getEnemiesKilled() >= 15) {
            if (AchievementManager.instance.toSave.getAchievements()[5] < 3) {
                AchievementManager.instance.toSave.getAchievements()[5] = 3;
            }
        } else if (AchievementManager.instance.toSave.getEnemiesKilled() >= 10) {
            if (AchievementManager.instance.toSave.getAchievements()[5] < 2) {
                AchievementManager.instance.toSave.getAchievements()[5] = 2;
            }
        } else if (AchievementManager.instance.toSave.getEnemiesKilled() >= 5) {
            if (AchievementManager.instance.toSave.getAchievements()[5] < 1) {
                AchievementManager.instance.toSave.getAchievements()[5] = 1;
            }
        }
    }
}