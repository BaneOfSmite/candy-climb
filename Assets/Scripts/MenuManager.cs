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
    public TextMeshProUGUI musicValue, effectValue, howToPlayTitle, howToPlayPageNumber;
    public AudioMixer volumeController;
    public GameObject optionMenu, achievementMenu, deathScreen, howToPlay;
    public GameObject[] howToPlayPages;
    private int howToPlayCurrent = 0;
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
                howToPlay.SetActive(false);
                GetComponent<Animator>().SetBool("Open", true);
                break;
            case 7: //Achievements
                optionMenu.SetActive(false);
                achievementMenu.SetActive(true);
                howToPlay.SetActive(false);
                GetComponent<Animator>().SetBool("Open", true);
                break;
            case 8: //Return to base Menu
                GetComponent<Animator>().SetBool("Open", false);
                break;
            //The rest is finished later, forgotten at the start...😅\\
            case 9: //How To Play
                howToPlayPageNumber.text = "Page " + (howToPlayCurrent + 1) + "/" + howToPlayPages.Length;
                optionMenu.SetActive(false);
                achievementMenu.SetActive(false);
                howToPlay.SetActive(true);
                GetComponent<Animator>().SetBool("Open", true);
                break;
            case 10: //In-game options button

                break;
        }
    }

    public void cycleHowToPlayPage(bool isForward) {
        if (isForward) {
            howToPlayCurrent++;
            if (howToPlayCurrent == howToPlayPages.Length) {
                howToPlayCurrent = 0;
            }
        } else {
            howToPlayCurrent--;
            if (howToPlayCurrent < 0) {
                howToPlayCurrent = howToPlayPages.Length - 1;
            }
        }
        switch (howToPlayCurrent) {
            case 0:
                howToPlayTitle.text = "Controls";
                break;
            case 1:
                howToPlayTitle.text = "Objective";
                break;
            case 2:
                howToPlayTitle.text = "Collectables";
                break;
            default:
                howToPlayTitle.text = "Sugar Rush";
                break;
        }
        howToPlayPageNumber.text = "Page " + (howToPlayCurrent + 1) + "/" + howToPlayPages.Length;
        for (int i = 0; i < howToPlayPages.Length; i++) {
            if (i != howToPlayCurrent) {
                howToPlayPages[i].SetActive(false);
            } else {
                howToPlayPages[i].SetActive(true);
            }
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
