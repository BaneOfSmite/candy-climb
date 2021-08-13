using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpManager : MonoBehaviour {
    public float[] timeLeft = new float[6];
    public MovementController _mc;
    public GameObject vig, barrier, effectDisplay;
    private IDictionary<GameObject, int> displayList = new Dictionary<GameObject, int>(); //Link timer to collectable
    public Sprite[] uiSourceImages;
    void Start() {
        for (int i = 0; i < effectDisplay.transform.childCount; i++) {
            displayList.Add(effectDisplay.transform.GetChild(i).gameObject, -1); //Set starting values of the Dictionary DisplayLists
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        for (int i = 0; i < timeLeft.Length; i++) {//Timer
            if (timeLeft[i] > 0) {
                if (!GameManager.instance.isPaused && Time.deltaTime < 1) {
                    timeLeft[i] -= Time.deltaTime;
                    for (int c = 0; c < effectDisplay.transform.childCount; c++) {
                        if (displayList[effectDisplay.transform.GetChild(c).gameObject] == i) {
                            effectDisplay.transform.GetChild(c).GetChild(0).GetComponent<TextMeshProUGUI>().text = Mathf.FloorToInt(timeLeft[i]).ToString(); //Update the timer in the HUD
                        }
                    }
                }
            } else if (timeLeft[i] > -10) { //Ensure the removal is only once so no duplication/Spam
                removeEffect(i);
                manageEffectDisplay(i, true);
            }
        }

    }

    public void addEffect(int type) {
        if (GameManager.instance.rushStatus == GameManager.sugarRushStatus.Charging) {
            switch (type) {
                case 0: //Macaron
                    foreach (GameObject e in GameObject.FindGameObjectsWithTag("Enemy")) {
                        if (Random.Range(0, 2) == 1) { //Thanos the enemies
                            Destroy(e);
                        }
                    }
                    break;
                case 1: //Doughnut
                    manageEffectDisplay(type, false);
                    if (timeLeft[type] < 1) {
                        Time.timeScale = 0.5f;
                    }
                    timeLeft[type] = 15;
                    break;
                case 2: //Cheesecake
                    manageEffectDisplay(type, false);
                    barrier.SetActive(true);
                    timeLeft[type] = 10;
                    break;
                case 3: //Apple
                    foreach (GameObject e in GameObject.FindGameObjectsWithTag("Platform")) { //Look for all spawned platforms and set it as "e"
                        e.transform.localScale -= new Vector3(e.transform.localScale.x * 0.2f, e.transform.localScale.y * 0.2f, e.transform.localScale.z * 0.2f); //Lower their localScale
                    }
                    break;
                case 4: //Carrot
                    manageEffectDisplay(type, false);
                    if (timeLeft[type] < 1) {
                        vig.SetActive(true);
                    }
                    timeLeft[type] = 5;
                    break;
                case 5: //Grape
                    manageEffectDisplay(type, false);
                    if (timeLeft[type] < 1) {
                        _mc.rotateSpeed = 15f;
                    }
                    timeLeft[type] = 5;
                    break;
            }
        }
    }
    public void removeEffect(int type) {
        timeLeft[type] = -10;
        switch (type) {
            case 0: //Macaron
                break;
            case 1: //Doughnut
                Time.timeScale = 1f;
                break;
            case 2: //Cheesecake
                barrier.SetActive(false);
                break;
            case 3: //Apple
                break;
            case 4: //Carrot
                vig.SetActive(false);
                break;
            case 5: //Grape
                _mc.rotateSpeed = 30f;
                break;
        }
    }

    private void manageEffectDisplay(int type, bool remove) { //Manages the HUD display on the left side to show when an effect is active
        for (int i = 0; i < effectDisplay.transform.childCount; i++) {
            if (remove) {
                if (displayList[effectDisplay.transform.GetChild(i).gameObject] == type) {
                    displayList[effectDisplay.transform.GetChild(i).gameObject] = -1; //Set it back to default value
                    effectDisplay.transform.GetChild(i).gameObject.SetActive(false);
                    return; //Break from function
                }
            } else {
                for (int a = 0; a < effectDisplay.transform.childCount; a++) {
                    if (displayList[effectDisplay.transform.GetChild(a).gameObject] == type) { //Prevent Duplication
                        return; //Break from function
                    }
                }
                if (displayList[effectDisplay.transform.GetChild(i).gameObject] == -1) { //Get the first UI child that is not used
                    displayList[effectDisplay.transform.GetChild(i).gameObject] = type;
                    effectDisplay.transform.GetChild(i).gameObject.SetActive(true); //Set the first UI object to true
                    effectDisplay.transform.GetChild(i).GetComponent<Image>().sprite = uiSourceImages[type]; //Change it to the correct collectable Image
                    return; //Break from function
                }
            }
        }
    }
}
