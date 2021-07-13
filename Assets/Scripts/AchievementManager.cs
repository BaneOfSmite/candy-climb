using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour {

    public SaveDataFile toSave = new SaveDataFile();
    public bool isMainMenu;
    public static AchievementManager instance;

    void Start() {
        instance = this;
        LoadData();
    }
    public void SaveData() {
        string json = JsonUtility.ToJson(toSave);
        PlayerPrefs.SetString("CandyClimbData", json);
    }

    private void LoadData() {
        if (PlayerPrefs.HasKey("CandyClimbData")) {
            string data = PlayerPrefs.GetString("CandyClimbData");
            toSave = JsonUtility.FromJson<SaveDataFile>(data);
        } else {
            toSave.setScore(0);
            toSave.setSugarRush(0);
            toSave.settotalCollected(0);
            toSave.sethealthyVomit(0);
            toSave.setenemiesKilled(0);
            //toSave.setAchievements
        }
    }
}
