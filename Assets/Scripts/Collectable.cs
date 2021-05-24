using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
    public enum collectableNames { Candy, Chocolate, Cupcake, Broccoli, Apple, Carrot, Null };
    public collectableNames currentType = collectableNames.Null;

    public Material[] color;

    void Start() {
        int collectable = Random.Range(0, 6);
        currentType = (collectableNames)collectable;

        //For Testing,
        if (collectable > 2) { //Bad Collectable
            GetComponent<MeshRenderer>().material = color[1];
        } else {
            GetComponent<MeshRenderer>().material = color[0];
        }

        /*switch (collectable) { //Set Mesh of the collectable
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 4:
                break;
            case 5:
                break;

        }*/
    }
}
