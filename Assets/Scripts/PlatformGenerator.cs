using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {
    public GameObject platform;

    private int toSpawn = 20;
    private float spawnHeight = 0;
    public float heightMultiplier = 3.85f;
    private void Start() {
        GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        heightMultiplier += ((gm.difficulity-1) * 1.5f);
        print(heightMultiplier);

        //toSpawn -= GameManager.instance.difficulity;
        for (int i = 0; i < toSpawn; i++) {
            GameObject spawned = Instantiate(platform);
            spawned.transform.parent = gameObject.transform;
            spawned.transform.localRotation = Quaternion.identity;
            spawned.transform.localScale = new Vector3(1, 1, 1);
            spawned.transform.localPosition = new Vector3(0, spawnHeight, Random.Range(-1.5f, 1.5f));
            spawnHeight += heightMultiplier; //3.85 slowly increase to 15
            if (spawnHeight > 73.2f) {
                break;
            }
        }
    }
}