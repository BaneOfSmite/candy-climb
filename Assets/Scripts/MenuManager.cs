using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MenuManager : MonoBehaviour {
    public Slider musicSlider, effectSlider;
    private AchievementManager _am;
    public TextMeshProUGUI musicValue, effectValue;
    public AudioMixer volumeController;
    public GameObject optionMenu, achievementMenu, deathScreen;
    public bool isMainMenu = true;

    void Start() {
        if (isMainMenu) {
            _am = GetComponent<AchievementManager>();
        } else {
            _am = AchievementManager.instance;
        }
        musicSlider.value = _am.toSave.getMusic();
        int toDisplay = Mathf.RoundToInt(((musicSlider.value + 80) / 80) * 100);
        musicValue.text = toDisplay.ToString();

        effectSlider.value = _am.toSave.getEffect();
        toDisplay = Mathf.RoundToInt(((effectSlider.value + 80) / 80) * 100);
        effectValue.text = toDisplay.ToString();
        updateAudioMixer();
    }

    public void inGameButton(int id) {
        switch (id) {
            //1-4 GameOver Buttons\\
            case 1: //Replay
                AchievementManager.instance.SaveData();
                SceneManager.LoadScene(1);
                break;
            case 2: //Achievements
                deathScreen.SetActive(false);
                achievementMenu.SetActive(true);
                break;
            case 3: //Home
                AchievementManager.instance.SaveData();
                SceneManager.LoadScene(0);
                break;
            case 4: //Return To Death Menu
                deathScreen.SetActive(true);
                achievementMenu.SetActive(false);
                break;

            //5-8 Main Menu's Button Controller\\
            case 5: //Play
                _am.SaveData();
                SceneManager.LoadSceneAsync(1);
                break;
            case 6: //Options
                optionMenu.SetActive(true);
                achievementMenu.SetActive(false);
                GetComponent<Animator>().SetBool("Open", true);
                break;
            case 7: //Achievements
                optionMenu.SetActive(false);
                achievementMenu.SetActive(true);
                GetComponent<Animator>().SetBool("Open", true);
                break;
            case 8: //Return to base Menu
                GetComponent<Animator>().SetBool("Open", false);
                break;
            //The rest is finished later, forgotten 😅\\
            case 9: //In-game options button
                
                break;
        }
    }

    public void setMusicVolume(float sliderValue) {
        int toDisplay = Mathf.RoundToInt(((musicSlider.value + 80) / 80) * 100);
        musicValue.text = toDisplay.ToString();
        volumeController.SetFloat("musicVolume", musicSlider.value);
        _am.toSave.setMusic(Mathf.RoundToInt(musicSlider.value));
    }
    public void setEffectVolume(float sliderValue) {
        int toDisplay = Mathf.RoundToInt(((effectSlider.value + 80) / 80) * 100);
        effectValue.text = toDisplay.ToString();
        volumeController.SetFloat("effectVolume", effectSlider.value);
        _am.toSave.setEffect(Mathf.RoundToInt(effectSlider.value));
    }

    private void updateAudioMixer() {
        volumeController.SetFloat("musicVolume", musicSlider.value);
        volumeController.SetFloat("effectVolume", effectSlider.value);
    }
}
