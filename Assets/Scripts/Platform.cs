using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    public GameObject[] collectablesObject;
    void Start() {
        if (Random.Range(0, 100) < 15) { //Rng chance to spawn collectable
            Vector3 spawnLoc = transform.position + new Vector3(0, 0.05f, 0);
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
        }

    }

    // Update is called once per frame
    void Update() {
        if (Mathf.FloorToInt(GameManager.instance.player.transform.position.y - transform.position.y) > 1) {//Clean Up
            Destroy(gameObject);
        }
    }
}
