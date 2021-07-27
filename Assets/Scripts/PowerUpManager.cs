using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
    public float[] timeLeft = new float[6];
    public MovementController _mc;
    public GameObject _vignette;
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
                    break;
                case 1: //Doughnut
                    if (timeLeft[type] < 1) {
                        Time.timeScale = 0.5f;
                    }
                    timeLeft[type] = 15;
                    break;
                case 2: //Cheesecake

                    timeLeft[type] = 10;
                    break;
                case 3: //Apple
                    break;
                case 4: //Carrot
                    if (timeLeft[type] < 1) {
                        _vignette.SetActive(true);
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
                break;
            case 3: //Apple
                break;
            case 4: //Carrot
                _vignette.SetActive(false);
                break;
            case 5: //Grape
                _mc.rotateSpeed = 30f;
                break;
        }
    }
}
