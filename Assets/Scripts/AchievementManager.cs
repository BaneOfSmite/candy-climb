using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AchievementManager : MonoBehaviour {

    public SaveDataFile toSave = new SaveDataFile();
    public bool isMainMenu;
    public static AchievementManager instance;
    public GameObject[] achievementTxtDisplays;

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
        if (PlayerPrefs.HasKey("CandyClimbData")) {
            string data = PlayerPrefs.GetString("CandyClimbData");
            toSave = JsonUtility.FromJson<SaveDataFile>(data);
        } else {
            toSave.setScore(0);
            toSave.setSugarRush(0);
            toSave.setTotalCollected(0);
            toSave.setHealthyVomit(0);
            toSave.setEnemiesKilled(0);
            toSave.setMusic(0);
            toSave.setEffect(0);
            toSave.setAchievements(new int[7]);
        }
        changeDisplay();
    }

    private void changeDisplay() {
        switch (toSave.getAchievements()[0]) {//Setting Text for Achievement 1
            case 0:
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().text = "Reached Height Of 50";
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
                achievementTxtDisplays[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
            case 1:
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().text = "Reached Height Of 50";
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                achievementTxtDisplays[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 100";
                break;
            case 2:
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().text = "Reached Height Of 100";
                achievementTxtDisplays[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 500";
                break;
            case 3:
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().text = "Reached Height Of 500";
                achievementTxtDisplays[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 1K";
                break;
            case 4:
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().text = "Reached Height Of 1K";
                achievementTxtDisplays[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 10K";
                break;
            case 5:
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().text = "Reached Height Of 10K";
                achievementTxtDisplays[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 100K";
                break;
            case 6:
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().text = "Reached Height Of 100K";
                achievementTxtDisplays[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 1M";
                break;
            case 7:
                achievementTxtDisplays[0].GetComponent<TextMeshProUGUI>().text = "Reached Height Of 1M";
                achievementTxtDisplays[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
        }
        switch (toSave.getAchievements()[1]) {//Setting Text for Achievement 2
            case 0:
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().text = "Sugar Rushed 1 Times";
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
                achievementTxtDisplays[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
            case 1:
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().text = "Sugar Rushed 1 Times";
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                achievementTxtDisplays[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 5";
                break;
            case 2:
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().text = "Sugar Rushed 5 Times";
                achievementTxtDisplays[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 10";
                break;
            case 3:
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().text = "Sugar Rushed 10 Times";
                achievementTxtDisplays[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 25";
                break;
            case 4:
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().text = "Sugar Rushed 25 Times";
                achievementTxtDisplays[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 50";
                break;
            case 5:
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().text = "Sugar Rushed 50 Times";
                achievementTxtDisplays[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 100";
                break;
            case 6:
                achievementTxtDisplays[1].GetComponent<TextMeshProUGUI>().text = "Sugar Rushed 100 Times";
                achievementTxtDisplays[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
        }
        switch (toSave.getAchievements()[2]) {//Setting Text for Achievement 3
            case 0:
                achievementTxtDisplays[2].GetComponent<TextMeshProUGUI>().text = "Ate 10 amount of Food";
                achievementTxtDisplays[2].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
                achievementTxtDisplays[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
            case 1:
                achievementTxtDisplays[2].GetComponent<TextMeshProUGUI>().text = "Ate 10 amount of Food";
                achievementTxtDisplays[2].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                achievementTxtDisplays[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 25";
                break;
            case 2:
                achievementTxtDisplays[2].GetComponent<TextMeshProUGUI>().text = "Ate 25 amount of Food";
                achievementTxtDisplays[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 50";
                break;
            case 3:
                achievementTxtDisplays[2].GetComponent<TextMeshProUGUI>().text = "Ate 50 amount of Food";
                achievementTxtDisplays[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 100";
                break;
            case 4:
                achievementTxtDisplays[2].GetComponent<TextMeshProUGUI>().text = "Ate 100 amount of Food";
                achievementTxtDisplays[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 500";
                break;
            case 5:
                achievementTxtDisplays[2].GetComponent<TextMeshProUGUI>().text = "Ate 500 amount of Food";
                achievementTxtDisplays[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
        }
        switch (toSave.getAchievements()[3]) {//Setting Text for Achievement 4
            case 0:
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().text = "Vomited 1 Times";
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
                achievementTxtDisplays[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
            case 1:
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().text = "Vomited 1 Times";
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                achievementTxtDisplays[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 5";
                break;
            case 2:
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().text = "Vomited 5 Times";
                achievementTxtDisplays[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 10";
                break;
            case 3:
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().text = "Vomited 10 Times";
                achievementTxtDisplays[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 25";
                break;
            case 4:
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().text = "Vomited 25 Times";
                achievementTxtDisplays[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 50";
                break;
            case 5:
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().text = "Vomited 50 Times";
                achievementTxtDisplays[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 100";
                break;
            case 6:
                achievementTxtDisplays[3].GetComponent<TextMeshProUGUI>().text = "Vomited 100 Times";
                achievementTxtDisplays[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
        }
        switch (toSave.getAchievements()[4]) {//Setting Text for Achievement 5
            case 0:
                achievementTxtDisplays[4].GetComponent<TextMeshProUGUI>().text = "Ate all different type of food without duplicate";
                achievementTxtDisplays[4].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
                break;
            case 1:
                achievementTxtDisplays[4].GetComponent<TextMeshProUGUI>().text = "Ate all different type of food without duplicate";
                achievementTxtDisplays[4].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                break;
        }
        switch (toSave.getAchievements()[5]) {//Setting Text for Achievement 6
            case 0:
                achievementTxtDisplays[5].GetComponent<TextMeshProUGUI>().text = "Killed 5 Fruit Guards";
                achievementTxtDisplays[5].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
                achievementTxtDisplays[5].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
            case 1:
                achievementTxtDisplays[5].GetComponent<TextMeshProUGUI>().text = "Killed 5 Fruit Guards";
                achievementTxtDisplays[5].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                achievementTxtDisplays[5].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 10";
                break;
            case 2:
                achievementTxtDisplays[5].GetComponent<TextMeshProUGUI>().text = "Killed 10 Fruit Guards";
                achievementTxtDisplays[5].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 15";
                break;
            case 3:
                achievementTxtDisplays[5].GetComponent<TextMeshProUGUI>().text = "Killed 15 Fruit Guards";
                achievementTxtDisplays[5].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 25";
                break;
            case 4:
                achievementTxtDisplays[5].GetComponent<TextMeshProUGUI>().text = "Killed 25 Fruit Guards";
                achievementTxtDisplays[5].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Next: 50";
                break;
            case 5:
                achievementTxtDisplays[5].GetComponent<TextMeshProUGUI>().text = "Killed 50 Fruit Guards";
                achievementTxtDisplays[5].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "";
                break;
        }
        switch (toSave.getAchievements()[6]) {//Setting Text for Achievement 7/ Hidden Achievement/Easter Egg
            case 0:
                achievementTxtDisplays[6].GetComponent<TextMeshProUGUI>().text = "Find the player on the main menu";
                achievementTxtDisplays[6].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 0.5f);
                break;
            case 1:
                achievementTxtDisplays[6].GetComponent<TextMeshProUGUI>().text = "Ate all different type of food without duplicate";
                achievementTxtDisplays[6].GetComponent<TextMeshProUGUI>().color = new Color(238f/255f, 198f/255f, 1, 1);
                break;
        }
    }
}
