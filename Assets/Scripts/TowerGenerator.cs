using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGenerator : MonoBehaviour {
    public GameObject currentWall;

    public GameObject wallPrefab;

    // Update is called once per frame
    void Update() {
        if (Camera.main.transform.position.y > currentWall.transform.position.y + 5) {
            Vector3 spawnLoc = currentWall.transform.position;
            spawnLoc += new Vector3(0, 7.82259f, 0);
            currentWall = Instantiate(wallPrefab, spawnLoc, transform.rotation * Quaternion.Euler(-90f, 0, 0f));
            currentWall.transform.parent = transform;
        }
    }
}
