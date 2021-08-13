using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private GameObject player;
    void Start() {
        player = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update() {
        if (player.transform.position.y > transform.position.y) { //Check if player is higher than the camera
            transform.position = new Vector3(0, player.transform.position.y, 0); //Move the camera Up
            transform.LookAt(player.transform); //Rotate towards the player
        }
    }
}
