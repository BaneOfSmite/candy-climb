using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
    public float[] timeLeft = new float[6];
    public MovementController _mc;
    public GameObject vig, barrier;
    void Start() {
    }

    // Update is called once per frame
    void FixedUpdate() {
        for (int i = 0; i < timeLeft.Length; i++) {//Timer
            if (timeLeft[i] > 0) {
                timeLeft[i] -= Time.unscaledDeltaTime;
            } else {
                removeEffect(i);
            }
        }

    }

    public void addEffect(int type) {
        if (GameManager.instance.rushStatus == GameManager.sugarRushStatus.Charging) {
            switch (type) {
                case 0: //Macaron
                    foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy")) {
                        if (Random.Range(0, 2) == 1) { //Thanos the enemies
                            Destroy(e);
                        }
                    }
                    break;
                case 1: //Doughnut
                    if (timeLeft[type] < 1) {
                        Time.timeScale = 0.5f;
                    }
                    timeLeft[type] = 15;
                    break;
                case 2: //Cheesecake
                    barrier.SetActive(true);
                    timeLeft[type] = 10;
                    break;
                case 3: //Apple
                    foreach (GameObject e in GameObject.FindGameObjectsWithTag("Platform")) {
                        e.transform.localScale -= new Vector3(e.transform.localScale.x * 0.2f, e.transform.localScale.y * 0.2f, e.transform.localScale.z * 0.2f);
                    }
                    break;
                case 4: //Carrot
                    if (timeLeft[type] < 1) {
                        vig.SetActive(true);
                    }
                    timeLeft[type] = 5;
                    break;
                case 5: //Grape
                    if (timeLeft[type] < 1) {
                        _mc.rotateSpeed = 15f;
                    }
                    timeLeft[type] = 5;
                    break;
            }
        }
    }
    public void removeEffect(int type) {
        timeLeft[type] = 0;
        switch (type) {
            case 0: //Macaron
                break;
            case 1: //Doughnut
                Time.timeScale = 1f;
                break;
            case 2: //Cheesecake
                barrier.SetActive(false);
                break;
            case 3: //Apple
                break;
            case 4: //Carrot
                vig.SetActive(false);
                break;
            case 5: //Grape
                _mc.rotateSpeed = 30f;
                break;
        }
    }
}
