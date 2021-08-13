using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
    public float rotateSpeed = 30f;
    public int isInvert = -1; //To Invert Controls when set to 1.

    void Update() {
        if (GameManager.instance.gameState != GameManager.gameStates.GameOver && !GameManager.instance.isPaused) {
            if (Application.platform == RuntimePlatform.Android) {
                transform.Rotate(new Vector3(0, Input.acceleration.x * isInvert, 0).normalized * rotateSpeed * Time.deltaTime); //Mobile rotational Control
            } else {
                transform.Rotate(new Vector3(0, Input.GetAxisRaw("Horizontal") * isInvert, 0) * rotateSpeed * Time.deltaTime); //PC's keyboard control
            }

            GameManager.instance.player.GetComponent<Animator>().SetBool("isWalking", (Input.acceleration.x > 0.1f && Input.acceleration.x < -0.1f || Input.GetAxisRaw("Horizontal") != 0)); //Set animation

        }
    }
}