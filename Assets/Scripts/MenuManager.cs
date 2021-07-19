using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public void inGameButton(int id) {
        switch (id) {
            //1-3 GameOver Buttons\\
            case 1: //Replay
                AchievementManager.instance.SaveData();
                SceneManager.LoadScene(1);
                break;
            case 2: //Achievements

                break;
            case 3: //Home
                AchievementManager.instance.SaveData();
                SceneManager.LoadScene(0);
                break;
            case 4:
                break;

            //5-7 Main Menu's Button Controller\\
            case 5: //Play
                SceneManager.LoadSceneAsync(1);
                break;
            case 6: //Options
                break;
            case 7: //Achievements
                break;
        }
    }
}
