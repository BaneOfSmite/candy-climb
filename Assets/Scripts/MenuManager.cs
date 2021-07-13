using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    // Start is called before the first frame update
    public void inGameButton(int id) {
        switch (id) {
            //1-3 GameOver Buttons
            case 1: //Replay
                AchievementManager.instance.SaveData();
                SceneManager.LoadScene(1);
                break;
            case 2: //Achievements
                
                break;
            case 3: //Home
                SceneManager.LoadScene(0);
                break;
            case 4:
                break;
        }
    }
}
