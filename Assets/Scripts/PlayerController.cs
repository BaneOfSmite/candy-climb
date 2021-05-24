using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float jumpHeight = 3f;
    //private float gravityValue = -9.81f;

    void Start() {
        //controller = GetComponent<CharacterController>();
    }

    void FixedUpdate() {
        if (GameManager.instance.score < transform.position.y) {
            GameManager.instance.score = Mathf.FloorToInt(transform.position.y);
        }

        if (Input.GetAxis("Horizontal") > 0) {
            GetComponent<SpriteRenderer>().flipX = false;
        } else if (Input.GetAxis("Horizontal") < 0) {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        /*groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0) {
            playerVelocity.y = 0f;
        }


        // Changes the height position of the player..
        if (GameManager.instance.gameState == GameManager.gameStates.inGame && groundedPlayer) {
            GetComponent<Animator>().SetTrigger("Jump");
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);*/
    }

    void OnTriggerEnter(Collider other) {
        if (GameManager.instance.gameState == GameManager.gameStates.inGame && other.gameObject.CompareTag("Platform")) {
            GetComponent<Animator>().SetTrigger("Jump");
            GetComponent<Rigidbody>().velocity = new Vector3(0, 1 * jumpHeight, 0);
        }
    }
}

