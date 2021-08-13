using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementManager : MonoBehaviour {

    public SaveDataFile toSave = new SaveDataFile();
    public bool isMainMenu;
    public static AchievementManager instance;
    public GameObject[] achievementTxtDisplays;
    public MenuManager _mm;

    void Start() {
        instance = this;
        LoadData();
    }
    public void SaveData() {
        string json = JsonUtility.ToJson(toSave);
        PlayerPrefs.SetString("CandyClimbData", json);
        changeDisplay();
    }

    private void LoadData() {
        if (PlayerPrefs.HasKey("CandyClimbData")) { //Checks if saved data exist
            string data = PlayerPrefs.GetString("CandyClimbData"); 
            toSave = JsonUtility.FromJson<SaveDataFile>(data); //Convert the string/JSON to Object
        } else { //Reset all values
            toSave.setScore(0);
            toSave.setSugarRush(0);
            toSave.setTotalCollected(0);
            toSave.setHealthyVomit(0);
            toSave.setEnemiesKilled(0);
            toSave.setMusic(1);
            toSave.setEffect(1);
            toSave.setAchievements(new int[7]);
        }
        //Set the music's value
        _mm.musicSlider.value = toSave.music;
        _mm.effectSlider.value = toSave.effect;
        int toDisplay = Mathf.FloorToInt(_mm.musicSlider.value * 100);
        _mm.musicValue.text = toDisplay.ToString();
        toDisplay = Mathf.FloorToInt(_mm.effectSlider.value * 100);
        _mm.effectValue.text = toDisplay.ToString();
        changeDisplay(); //Update the Achievement UI
    }

    private void changeDisplay() {
        switch (toSave.getAchievements()[0]) {//Setting Text for Achievement 1
            case 0:
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().text = "Reach Height Of 50";
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
                achievementTxtDisplays[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
            case 1:
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().text = "Reach Height Of 50";
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                achievementTxtDisplays[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 100";
                break;
            case 2:
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().text = "Reach Height Of 100";
                achievementTxtDisplays[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 500";
                break;
            case 3:
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().text = "Reach Height Of 500";
                achievementTxtDisplays[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 1K";
                break;
            case 4:
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().text = "Reach Height Of 1K";
                achievementTxtDisplays[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 10K";
                break;
            case 5:
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().text = "Reach Height Of 10K";
                achievementTxtDisplays[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 100K";
                break;
            case 6:
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().text = "Reach Height Of 100K";
                achievementTxtDisplays[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 1M";
                break;
            case 7:
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().text = "Reach Height Of 1M";
                achievementTxtDisplays[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
        }
        switch (toSave.getAchievements()[1]) {//Setting Text for Achievement 2
            case 0:
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().text = "Sugar Rush 1 Times";
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
                achievementTxtDisplays[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
            case 1:
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().text = "Sugar Rush 1 Times";
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                achievementTxtDisplays[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 5";
                break;
            case 2:
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().text = "Sugar Rush 5 Times";
                achievementTxtDisplays[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 10";
                break;
            case 3:
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().text = "Sugar Rush 10 Times";
                achievementTxtDisplays[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 25";
                break;
            case 4:
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().text = "Sugar Rush 25 Times";
                achievementTxtDisplays[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 50";
                break;
            case 5:
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().text = "Sugar Rush 50 Times";
                achievementTxtDisplays[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 100";
                break;
            case 6:
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().text = "Sugar Rush 100 Times";
                achievementTxtDisplays[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
        }
        switch (toSave.getAchievements()[2]) {//Setting Text for Achievement 3
            case 0:
                achievementTxtDisplays[2].GetComponent<TextMeshProUGUI>().text = "Eat 10 amount of Food";
                achievementTxtDisplays[2].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
                achievementTxtDisplays[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
            case 1:
                achievementTxtDisplays[2].GetComponent<TextMeshProUGUI>().text = "Eat 10 amount of Food";
                achievementTxtDisplays[2].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                achievementTxtDisplays[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 25";
                break;
            case 2:
                achievementTxtDisplays[2].GetComponent<TextMeshProUGUI>().text = "Eat 25 amount of Food";
                achievementTxtDisplays[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 50";
                break;
            case 3:
                achievementTxtDisplays[2].GetComponent<TextMeshProUGUI>().text = "Eat 50 amount of Food";
                achievementTxtDisplays[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 100";
                break;
            case 4:
                achievementTxtDisplays[2].GetComponent<TextMeshProUGUI>().text = "Eat 100 amount of Food";
                achievementTxtDisplays[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 500";
                break;
            case 5:
                achievementTxtDisplays[2].GetComponent<TextMeshProUGUI>().text = "Eat 500 amount of Food";
                achievementTxtDisplays[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
        }
        switch (toSave.getAchievements()[3]) {//Setting Text for Achievement 4
            case 0:
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().text = "Vomit 1 Times";
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
                achievementTxtDisplays[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
            case 1:
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().text = "Vomit 1 Times";
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                achievementTxtDisplays[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 5";
                break;
            case 2:
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().text = "Vomit 5 Times";
                achievementTxtDisplays[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 10";
                break;
            case 3:
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().text = "Vomit 10 Times";
                achievementTxtDisplays[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 25";
                break;
            case 4:
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().text = "Vomit 25 Times";
                achievementTxtDisplays[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 50";
                break;
            case 5:
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().text = "Vomit 50 Times";
                achievementTxtDisplays[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 100";
                break;
            case 6:
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().text = "Vomit 100 Times";
                achievementTxtDisplays[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
        }
        switch (toSave.getAchievements()[4]) {//Setting Text for Achievement 5
            case 0:
                achievementTxtDisplays[4].GetComponent<TextMeshProUGUI>().text = "Eat all different type of food without duplicate";
                achievementTxtDisplays[4].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
                break;
            case 1:
                achievementTxtDisplays[4].GetComponent<TextMeshProUGUI>().text = "Eat all different type of food without duplicate";
                achievementTxtDisplays[4].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                break;
        }
        switch (toSave.getAchievements()[5]) {//Setting Text for Achievement 6
            case 0:
                achievementTxtDisplays[5].GetComponent<TextMeshProUGUI>().text = "Kill 5 Fruit Guards";
                achievementTxtDisplays[5].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
                achievementTxtDisplays[5].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
            case 1:
                achievementTxtDisplays[5].GetComponent<TextMeshProUGUI>().text = "Kill 5 Fruit Guards";
                achievementTxtDisplays[5].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                achievementTxtDisplays[5].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 10";
                break;
            case 2:
                achievementTxtDisplays[5].GetComponent<TextMeshProUGUI>().text = "Kill 10 Fruit Guards";
                achievementTxtDisplays[5].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 15";
                break;
            case 3:
                achievementTxtDisplays[5].GetComponent<TextMeshProUGUI>().text = "Kill 15 Fruit Guards";
                achievementTxtDisplays[5].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 25";
                break;
            case 4:
                achievementTxtDisplays[5].GetComponent<TextMeshProUGUI>().text = "Kill 25 Fruit Guards";
                achievementTxtDisplays[5].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 50";
                break;
            case 5:
                achievementTxtDisplays[5].GetComponent<TextMeshProUGUI>().text = "Kill 50 Fruit Guards";
                achievementTxtDisplays[5].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
        }
        switch (toSave.getAchievements()[6]) {//Setting Text for Achievement 7/ Hidden Achievement/Easter Egg
            case 0:
                achievementTxtDisplays[6].GetComponent<TextMeshProUGUI>().text = "Find the player on the main menu";
                achievementTxtDisplays[6].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
                break;
            case 1:
                achievementTxtDisplays[6].GetComponent<TextMeshProUGUI>().text = "Find the player on the main menu";
                achievementTxtDisplays[6].GetComponent<TextMeshProUGUI>().color = new Color(238f / 255f, 198f / 255f, 1, 1);
                break;
        }
    }

    public void findPlayerInMenu() {
        toSave.getAchievements()[6] = 1;
        changeDisplay();
    }
}
