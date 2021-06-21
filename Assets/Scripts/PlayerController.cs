using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float jumpHeight = 3f;

    public AudioClip[] clips;

    //private float gravityValue = -9.81f;

    void Start() {
        //controller = GetComponent<CharacterController>();
    }

    void FixedUpdate() {
        if (GameManager.instance.score < transform.position.y) {
            GameManager.instance.score = Mathf.FloorToInt(transform.position.y);
        }

        if (Input.GetAxis("Horizontal") > 0 || Input.acceleration.x > 0.1) {
            GetComponent<SpriteRenderer>().flipX = false;
        } else if (Input.GetAxis("Horizontal") < 0 || Input.acceleration.x < -0.1) {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void OnTriggerEnter(Collider other) {
        print(other.GetType().Name);
        if (GameManager.instance.gameState == GameManager.gameStates.inGame) {
            if (other.gameObject.CompareTag("Platform")) {
                GetComponent<Animator>().SetTrigger("Jump");

                if (GetComponent<Rigidbody>().velocity.y < 0) {
                    playSound(clips[0]);
                } else {
                    playSound(clips[1]);
                }

                GetComponent<Rigidbody>().velocity = new Vector3(0, 1 * jumpHeight, 0);
            } else if (other.gameObject.CompareTag("Collectable")) {
                switch (other.gameObject.GetComponent<Collectable>().currentType) {

                    //Good Collectables\\
                    case Collectable.collectableNames.Cheesecake:
                        playSound(clips[3]);
                        //Sugar Rush Cheesecake
                        break;
                    case Collectable.collectableNames.Doughnut:
                        playSound(clips[3]);
                        //Sugar Rush Doughnut
                        break;
                    case Collectable.collectableNames.Macaron:
                        playSound(clips[3]);
                        //Sugar Rush Macaron
                        break;

                    //Bad Collectables\\
                    case Collectable.collectableNames.Apple:
                        playSound(clips[4]);
                        //Sugar Rush Apple
                        break;
                    case Collectable.collectableNames.Carrot:
                        playSound(clips[4]);
                        //Sugar Rush Carrot
                        break;
                    case Collectable.collectableNames.Grape:
                        playSound(clips[4]);
                        //Sugar Rush Grape
                        break;

                }
                //Spawn Collect Particle
                Destroy(other.gameObject);

            }
        }
    }

    private void playSound(AudioClip _clip) {
        GetComponent<AudioSource>().PlayOneShot(_clip);
    }
}

