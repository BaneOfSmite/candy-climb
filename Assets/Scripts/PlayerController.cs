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
        if (GameManager.instance.gameState == GameManager.gameStates.inGame && other.gameObject.CompareTag("Platform")) {
            GetComponent<Animator>().SetTrigger("Jump");

            if (GetComponent<Rigidbody>().velocity.y < 0) {
                GetComponent<AudioSource>().PlayOneShot(clips[0]);
            } else {
                GetComponent<AudioSource>().PlayOneShot(clips[1]);
            }
            
            GetComponent<Rigidbody>().velocity = new Vector3(0, 1 * jumpHeight, 0);
        }
    }
}

