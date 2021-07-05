using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour {
    public GameObject[] platform;
    public Material[] colorRng;
    private int toSpawn = 20;
    private float spawnHeight = 0;
    public float heightMultiplier = 4f;

    private float scaleOffSet = 0.15f;
    private void Start() {
        GameManager gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>(); //Difficulity Scaling
        heightMultiplier += ((gm.difficulity - 1) * 1.5f); //Multiplier

        for (int i = 0; i < toSpawn; i++) {
            int cloudId = Random.Range(0, platform.Length); //Random cloud model

            GameObject spawned = Instantiate(platform[cloudId]);

            //Managing the newly spawned cloud\\
            spawned.GetComponent<MeshRenderer>().material = colorRng[Random.Range(0, colorRng.Length)]; //Random Cloud Color
            spawned.transform.parent = gameObject.transform;
            spawned.transform.localRotation = Quaternion.identity;
            spawned.transform.Rotate(0, 90, 0);
            spawned.transform.localScale = new Vector3(spawned.transform.localScale.x * scaleOffSet, spawned.transform.localScale.y * scaleOffSet, spawned.transform.localScale.z * scaleOffSet);
            spawned.transform.localPosition = new Vector3(0, spawnHeight, Random.Range(-1.5f, 1.5f));
            spawnHeight += heightMultiplier; //Applying the height/difficulity multiplier
            if (spawnHeight > 73.2f) {
                break;
            }
        }
    }
}