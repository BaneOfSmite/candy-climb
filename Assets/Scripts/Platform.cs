using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    public GameObject[] collectablesObject;
    public GameObject[] enemies;
    void Start() {
        Vector3 spawnLoc;
        int rngPercentage = Random.Range(0, 100);
        if (rngPercentage < 16) { //Rng chance to spawn collectable
            if (rngPercentage < 8f) {
                spawnLoc = transform.position + new Vector3(0, 0.05f, 0);
                GameObject spawned = Instantiate(collectablesObject[Random.Range(0, collectablesObject.Length)], spawnLoc, Quaternion.identity);
                spawned.transform.LookAt(new Vector3(0, transform.position.y, 0));

                if (spawned.GetComponent<Collectable>().currentType != Collectable.collectableNames.Macaron) {
                    spawned.transform.Rotate(new Vector3(10, 50, 10));

                    if (spawned.GetComponent<Collectable>().currentType == Collectable.collectableNames.Carrot
                    || spawned.GetComponent<Collectable>().currentType == Collectable.collectableNames.Grape
                    || spawned.GetComponent<Collectable>().currentType == Collectable.collectableNames.Apple) {
                        spawned.transform.position += new Vector3(0, 0.1f, 0);
                    } else if (spawned.GetComponent<Collectable>().currentType == Collectable.collectableNames.Cheesecake) {
                        spawned.transform.position += new Vector3(0, 0.05f, 0);
                    }
                }
                spawned.transform.SetParent(transform);
            } else {
                spawnLoc = transform.position + new Vector3(0, 0.2f, 0);
                GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], spawnLoc, Quaternion.identity);
                enemy.transform.LookAt(new Vector3(0, transform.position.y, 0));
                enemy.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                enemy.transform.SetParent(transform);
            }
        }

    }

    // Update is called once per frame
    void Update() {
        if (Mathf.FloorToInt(GameManager.instance.player.transform.position.y - transform.position.y) > 1) {//Clean Up
            Destroy(gameObject);
        }
    }
}
