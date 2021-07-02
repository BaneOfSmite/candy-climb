using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
    public float rotateSpeed = 30f;
    public int isInvert = -1;
    
    // Update is called once per frame
    void Update() {
        if (GameManager.instance.gameState != GameManager.gameStates.GameOver) {
            //transform.Rotate(new Vector3(0, Input.acceleration.x * isInvert, 0).normalized * rotateSpeed * Time.deltaTime); //Mobile rotational Control

            transform.Rotate(new Vector3(0, Input.GetAxisRaw("Horizontal") * isInvert, 0) * rotateSpeed * Time.deltaTime); //PC's keyboard control
            GameManager.instance.player.GetComponent<Animator>().SetBool("isWalking", (Input.acceleration.x > 0.1f && Input.acceleration.x < -0.1f || Input.GetAxisRaw("Horizontal") != 0));

        }
    }
}