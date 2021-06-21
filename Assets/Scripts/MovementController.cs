using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {
    public float rotateSpeed = 0.1f;

    // Update is called once per frame
    void Update() {
        if (GameManager.instance.gameState != GameManager.gameStates.GameOver) {
            //transform.Rotate(new Vector3(0, Input.acceleration.x * -1, 0) * 0.3f); //Mobile rotational Control


            transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * -1, 0) * rotateSpeed); //PC's keyboard control
        }
    }
}
