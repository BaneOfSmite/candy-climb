﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float jumpHeight = 5f;

    public AudioClip[] clips;
    public static PlayerController instance;
    public bool[] collectWithoutDupe = new bool[6];

    void Start() {
        instance = this;
    }

    void FixedUpdate() {
        if (GameManager.instance.score < transform.position.y) {
            GameManager.instance.score = Mathf.FloorToInt(transform.position.y);
        }

        if (Input.GetAxis("Horizontal") > 0 || Input.acceleration.x > 0.1) {
            GetComponent<SpriteRenderer>().flipX = false;
            transform.GetChild(1).localPosition = new Vector3(0.1f, -0.07f, 0);
            transform.GetChild(1).localEulerAngles = new Vector3(0, 0, 0);
        } else if (Input.GetAxis("Horizontal") < 0 || Input.acceleration.x < -0.1) {
            GetComponent<SpriteRenderer>().flipX = true;
            transform.GetChild(1).localPosition = new Vector3(-0.1f, -0.07f, 0);
            transform.GetChild(1).localEulerAngles = new Vector3(0, 180, 0);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (GameManager.instance.gameState == GameManager.gameStates.inGame) {
            if (other.gameObject.CompareTag("Platform")) {
                GetComponent<Animator>().SetTrigger("Jump");

                if (GetComponent<Rigidbody>().velocity.y < 0) {
                    playSound(clips[0]);
                } else {
                    playSound(clips[1]);
                }

                GetComponent<Rigidbody>().velocity = new Vector3(0, 1 * jumpHeight, 0);
            }
        }
    }

    private void playSound(AudioClip _clip) {
        GetComponent<AudioSource>().PlayOneShot(_clip);
    }

    public void collectedCollectable(GameObject _collectable) {
        AchievementManager.instance.toSave.setTotalCollected(AchievementManager.instance.toSave.getTotalCollected() + 1);
        if (AchievementManager.instance.toSave.getAchievements()[4] < 1) {
            if (!collectWithoutDupe[(int)_collectable.GetComponent<Collectable>().currentType]) {
                collectWithoutDupe[(int)_collectable.GetComponent<Collectable>().currentType] = true;
                bool achieved = true;
                for (int i = 0; i < collectWithoutDupe.Length; i++) {
                    if (!collectWithoutDupe[i]) {
                        achieved = false;
                    }
                }
                if (achieved) {

                    AchievementManager.instance.toSave.getAchievements()[4] = 1;

                }
            } else {
                collectWithoutDupe = new bool[6];
            }
        }
        switch (_collectable.GetComponent<Collectable>().currentType) {
            //Good Collectables\\
            case Collectable.collectableNames.Cheesecake:
                playSound(clips[3]);
                GameManager.instance.AddToSugarRush(7.5f);
                //Sugar Rush Cheesecake
                break;
            case Collectable.collectableNames.Doughnut:
                playSound(clips[3]);
                GameManager.instance.AddToSugarRush(5);
                //Sugar Rush Doughnut
                break;
            case Collectable.collectableNames.Macaron:
                playSound(clips[3]);
                GameManager.instance.AddToSugarRush(10);
                //Sugar Rush Macaron
                break;

            //Bad Collectables\\
            case Collectable.collectableNames.Apple:
                playSound(clips[4]);
                GameManager.instance.AddToSugarRush(-10);
                //Sugar Rush Apple
                break;
            case Collectable.collectableNames.Carrot:
                playSound(clips[4]);
                GameManager.instance.AddToSugarRush(-12.5f);
                //Sugar Rush Carrot
                break;
            case Collectable.collectableNames.Grape:
                playSound(clips[4]);
                GameManager.instance.AddToSugarRush(-15);
                //Sugar Rush Grape
                break;

        }
        //Spawn Collect Particle
        Destroy(_collectable);
    }
}

