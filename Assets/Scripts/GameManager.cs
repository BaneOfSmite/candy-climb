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
    public GameObject player, gameOverUI, sugarRushBar, newBestScoreUI; //Player's Object
    public float sugarRushValue = 50;
    public ParticleSystem[] _particleSystems;

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
            GameOver();
        }
    }

    private void updateScore() {
        scoreUi.text = "Score: " + scoreFormatter(score);
        gameOverCurrent.text = "Current Score: " + scoreFormatter(score);
    }
    public void GameOver() {
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
        AchievementManager.instance.SaveData();
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
            AchievementManager.instance.toSave.setSugarRush(AchievementManager.instance.toSave.getSugarRush() + 1);
            //_particleSystems[0].Stop();
            float currentJumpHeight = player.GetComponent<PlayerController>().jumpHeight;
            player.GetComponent<PlayerController>().jumpHeight = 7.5f;
            volume.profile.GetSetting<ChromaticAberration>().intensity.value = 1;
            while (sugarRushValue >= 25) {
                yield return new WaitForSecondsRealtime(0.2f);
                sugarRushValue--;
                sugarRushBar.GetComponent<Slider>().value = Mathf.Clamp(sugarRushValue, 0, 100);
            }
            volume.profile.GetSetting<ChromaticAberration>().intensity.value = 0;
            player.GetComponent<PlayerController>().jumpHeight = currentJumpHeight;
            //_particleSystems[0].Stop();

        } else { //Bad
            AchievementManager.instance.toSave.setHealthyVomit(AchievementManager.instance.toSave.getHealthyVomit() + 1);
            _particleSystems[1].Play();
            GameObject.FindGameObjectWithTag("MovementController").GetComponent<MovementController>().isInvert = 1;
            volume.profile.GetSetting<ColorGrading>().colorFilter.value = new Color(124f / 255f, 200f / 255f, 124f / 255f);
            while (sugarRushValue <= 50) {
                yield return new WaitForSecondsRealtime(0.2f);
                sugarRushValue++;
                sugarRushBar.GetComponent<Slider>().value = Mathf.Clamp(sugarRushValue, 0, 100);
            }
            GameObject.FindGameObjectWithTag("MovementController").GetComponent<MovementController>().isInvert = -1;
            volume.profile.GetSetting<ColorGrading>().colorFilter.value = new Color(1, 1, 1);
            _particleSystems[1].Stop();
        }
        rushStatus = sugarRushStatus.Charging;
    }
    public string scoreFormatter(float _score) {
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

    private void detectAchievement() {
        //Reach Height Of X\\
        if (score > 1000000) {
            if (AchievementManager.instance.toSave.getAchievements()[0] != 7) {
                AchievementManager.instance.toSave.getAchievements()[0] = 7;
            }
        } else if (score > 100000) {
            if (AchievementManager.instance.toSave.getAchievements()[0] < 6) {
                AchievementManager.instance.toSave.getAchievements()[0] = 6;
            }
        } else if (score > 10000) {
            if (AchievementManager.instance.toSave.getAchievements()[0] < 5) {
                AchievementManager.instance.toSave.getAchievements()[0] = 5;
            }
        } else if (score > 1000) {
            if (AchievementManager.instance.toSave.getAchievements()[0] < 4) {
                AchievementManager.instance.toSave.getAchievements()[0] = 4;
            }
        } else if (score > 500) {
            if (AchievementManager.instance.toSave.getAchievements()[0] < 3) {
                AchievementManager.instance.toSave.getAchievements()[0] = 3;
            }
        } else if (score > 100) {
            if (AchievementManager.instance.toSave.getAchievements()[0] < 2) {
                AchievementManager.instance.toSave.getAchievements()[0] = 2;
            }
        } else if (score > 50) {
            if (AchievementManager.instance.toSave.getAchievements()[0] < 1) {
                AchievementManager.instance.toSave.getAchievements()[0] = 1;
            }
        }
        //Sugar Rush X Times\\
        if (AchievementManager.instance.toSave.getSugarRush() > 100) {
            if (AchievementManager.instance.toSave.getAchievements()[1] != 6) {
                AchievementManager.instance.toSave.getAchievements()[1] = 6;
            }
        } else if (AchievementManager.instance.toSave.getSugarRush() > 50) {
            if (AchievementManager.instance.toSave.getAchievements()[1] < 5) {
                AchievementManager.instance.toSave.getAchievements()[1] = 5;
            }
        } else if (AchievementManager.instance.toSave.getSugarRush() > 25) {
            if (AchievementManager.instance.toSave.getAchievements()[1] < 4) {
                AchievementManager.instance.toSave.getAchievements()[1] = 4;
            }
        } else if (AchievementManager.instance.toSave.getSugarRush() > 10) {
            if (AchievementManager.instance.toSave.getAchievements()[1] < 3) {
                AchievementManager.instance.toSave.getAchievements()[1] = 3;
            }
        } else if (AchievementManager.instance.toSave.getSugarRush() > 5) {
            if (AchievementManager.instance.toSave.getAchievements()[1] < 2) {
                AchievementManager.instance.toSave.getAchievements()[1] = 2;
            }
        } else if (AchievementManager.instance.toSave.getSugarRush() > 1) {
            if (AchievementManager.instance.toSave.getAchievements()[1] < 1) {
                AchievementManager.instance.toSave.getAchievements()[1] = 1;
            }
        }
        //Collected Collectable X Times\\
        if (AchievementManager.instance.toSave.getTotalCollected() > 500) {
            if (AchievementManager.instance.toSave.getAchievements()[2] != 5) {
                AchievementManager.instance.toSave.getAchievements()[2] = 5;
            }
        } else if (AchievementManager.instance.toSave.getTotalCollected() > 100) {
            if (AchievementManager.instance.toSave.getAchievements()[2] < 4) {
                AchievementManager.instance.toSave.getAchievements()[2] = 4;
            }
        } else if (AchievementManager.instance.toSave.getTotalCollected() > 50) {
            if (AchievementManager.instance.toSave.getAchievements()[2] < 3) {
                AchievementManager.instance.toSave.getAchievements()[2] = 3;
            }
        } else if (AchievementManager.instance.toSave.getTotalCollected() > 25) {
            if (AchievementManager.instance.toSave.getAchievements()[2] < 2) {
                AchievementManager.instance.toSave.getAchievements()[2] = 2;
            }
        } else if (AchievementManager.instance.toSave.getTotalCollected() > 10) {
            if (AchievementManager.instance.toSave.getAchievements()[2] < 1) {
                AchievementManager.instance.toSave.getAchievements()[2] = 1;
            }
        }
        //Vomit X Times\\
        if (AchievementManager.instance.toSave.getHealthyVomit() > 100) {
            if (AchievementManager.instance.toSave.getAchievements()[3] != 6) {
                AchievementManager.instance.toSave.getAchievements()[3] = 6;
            }
        } else if (AchievementManager.instance.toSave.getHealthyVomit() > 50) {
            if (AchievementManager.instance.toSave.getAchievements()[3] < 5) {
                AchievementManager.instance.toSave.getAchievements()[3] = 5;
            }
        } else if (AchievementManager.instance.toSave.getHealthyVomit() > 25) {
            if (AchievementManager.instance.toSave.getAchievements()[3] < 4) {
                AchievementManager.instance.toSave.getAchievements()[3] = 4;
            }
        } else if (AchievementManager.instance.toSave.getHealthyVomit() > 10) {
            if (AchievementManager.instance.toSave.getAchievements()[3] < 3) {
                AchievementManager.instance.toSave.getAchievements()[3] = 3;
            }
        } else if (AchievementManager.instance.toSave.getHealthyVomit() > 5) {
            if (AchievementManager.instance.toSave.getAchievements()[3] < 2) {
                AchievementManager.instance.toSave.getAchievements()[3] = 2;
            }
        } else if (AchievementManager.instance.toSave.getHealthyVomit() > 1) {
            if (AchievementManager.instance.toSave.getAchievements()[3] < 1) {
                AchievementManager.instance.toSave.getAchievements()[3] = 1;
            }
        }
        //Killed X Fruit Guards\\
        if (AchievementManager.instance.toSave.getEnemiesKilled() > 50) {
            if (AchievementManager.instance.toSave.getAchievements()[4] != 5) {
                AchievementManager.instance.toSave.getAchievements()[4] = 5;
            }
        } else if (AchievementManager.instance.toSave.getEnemiesKilled() > 25) {
            if (AchievementManager.instance.toSave.getAchievements()[4] < 4) {
                AchievementManager.instance.toSave.getAchievements()[4] = 4;
            }
        } else if (AchievementManager.instance.toSave.getEnemiesKilled() > 15) {
            if (AchievementManager.instance.toSave.getAchievements()[4] < 3) {
                AchievementManager.instance.toSave.getAchievements()[4] = 3;
            }
        } else if (AchievementManager.instance.toSave.getEnemiesKilled() > 10) {
            if (AchievementManager.instance.toSave.getAchievements()[4] < 2) {
                AchievementManager.instance.toSave.getAchievements()[4] = 2;
            }
        } else if (AchievementManager.instance.toSave.getEnemiesKilled() > 5) {
            if (AchievementManager.instance.toSave.getAchievements()[4] < 1) {
                AchievementManager.instance.toSave.getAchievements()[4] = 1;
            }
        }
    }
}