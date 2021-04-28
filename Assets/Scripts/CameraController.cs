using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private GameObject player;
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update() {
        if (player.transform.position.y > transform.position.y) {
            transform.position = new Vector3(0, player.transform.position.y, 0);
            transform.LookAt(player.transform);
        }
    }
}
