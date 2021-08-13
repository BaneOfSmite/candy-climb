using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
    public enum collectableNames { Macaron, Doughnut, Cheesecake, Apple, Carrot, Grape, Null };
    public collectableNames currentType = collectableNames.Null;

    public Material[] color;

    void Start() {
        if (currentType == collectableNames.Null) {
            //If the collectable type is not set, delete it
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            PlayerController.instance.collectedCollectable(gameObject); //When player touches the collectable, trigger the collectedCollectable function in playerController Script
        }
    }
}
