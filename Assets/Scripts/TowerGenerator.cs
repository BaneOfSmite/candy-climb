using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGenerator : MonoBehaviour {
    public GameObject currentWall;

    public GameObject wallPrefab;

    // Update is called once per frame
    void Update() {
        if (Camera.main.transform.position.y > currentWall.transform.position.y + 5) { //If the camera is going too high, spawn the next set of walls
            Vector3 spawnLoc = currentWall.transform.position;
            spawnLoc += new Vector3(0, 7.82259f, 0); //Spawn Location Offset from previous wall.
            currentWall = Instantiate(wallPrefab, spawnLoc, transform.rotation * Quaternion.Euler(-90f, 0, 0f)); //Set new current wall as well as spawning it with a rotation of -90 degrees
            currentWall.transform.SetParent(transform);
        }
    }
}
