using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class MenuManager : MonoBehaviour {
    public Slider musicSlider, effectSlider;
    public AchievementManager _am;
    public TextMeshProUGUI musicValue, effectValue, howToPlayTitle, howToPlayPageNumber;
    public AudioMixer volumeController;
    public GameObject optionMenu, achievementMenu, deathScreen, howToPlay;
    public GameObject[] howToPlayPages;
    private int howToPlayCurrent = 0;
    public bool isMainMenu = true;
    private float _currentTimeScale = 1;

    void Start() {
        updateAudioMixer();
    }

    public void inGameButton(int id) {
        switch (id) {
            //Game Scene Buttons\\
            case 1: //Replay
                Time.timeScale = 1;
                _am.SaveData();
                SceneManager.LoadScene(1);
                break;
            case 2: //Achievements
                deathScreen.SetActive(false);
                achievementMenu.SetActive(true);
                break;
            case 3: //Home
                Time.timeScale = 1;
                _am.SaveData();
                SceneManager.LoadScene(0);
                break;
            case 4: //Return To Death Menu
                deathScreen.SetActive(true);
                achievementMenu.SetActive(false);
                break;
            case 10: //Pause
                Time.timeScale = 0;
                GameManager.instance.isPaused = true;
                optionMenu.SetActive(true);
                break;
            case 11: //Resume
                Time.timeScale = _currentTimeScale;
                GameManager.instance.isPaused = false;
                optionMenu.SetActive(false);
                break;



            //Main Menu's Button\\
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
            case 9: //How To Play
                howToPlayPageNumber.text = "Page " + (howToPlayCurrent + 1) + "/" + howToPlayPages.Length;
                optionMenu.SetActive(false);
                achievementMenu.SetActive(false);
                howToPlay.SetActive(true);
                GetComponent<Animator>().SetBool("Open", true);
                break;
            case 12: //Reset Achievement
                for (int i = 0; i < _am.toSave.Achievements.Length; i++) {
                    _am.toSave.Achievements[i] = 0;
                }
                _am.toSave.setScore(0);
                _am.toSave.setSugarRush(0);
                _am.toSave.setTotalCollected(0);
                _am.toSave.setHealthyVomit(0);
                _am.toSave.setEnemiesKilled(0);
                _am.SaveData();
                SceneManager.LoadScene(0);
                break;
        }
    }

    public void cycleHowToPlayPage(bool isForward) { //Manages the HowToPlay Menu
        if (isForward) { //Is forward button pressed
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
        switch (howToPlayCurrent) { //Switches the title
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
        for (int i = 0; i < howToPlayPages.Length; i++) { //Change the page
            if (i != howToPlayCurrent) {
                howToPlayPages[i].SetActive(false);
            } else {
                howToPlayPages[i].SetActive(true);
            }
        }
    }

    public void setMusicVolume(float sliderValue) { //Updating the music volume's UI and text element
        int toDisplay = Mathf.FloorToInt(musicSlider.value * 100);
        musicValue.text = toDisplay.ToString();
        _am.toSave.setMusic(musicSlider.value);
        updateAudioMixer();
    }
    public void setEffectVolume(float sliderValue) { //Updating the effect volume's UI and text element
        int toDisplay = Mathf.FloorToInt(effectSlider.value * 100);
        effectValue.text = toDisplay.ToString();
        _am.toSave.setEffect(effectSlider.value);
        updateAudioMixer();
    }

    private void updateAudioMixer() { //Update the actual volume in the AudioMixer
        volumeController.SetFloat("musicVolume", Mathf.Log10(musicSlider.value) * 20);
        volumeController.SetFloat("effectVolume", Mathf.Log10(effectSlider.value) * 20);
    }
}
