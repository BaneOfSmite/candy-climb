using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
    public enum collectableNames { Macaron, Doughnut, Cheesecake, Apple, Carrot, Grape, Null };
    public collectableNames currentType = collectableNames.Null;

    public Material[] color;

    void Start() {
        if (currentType == collectableNames.Null) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            PlayerController.instance.collectedCollectable(gameObject);
        }
    }
}
