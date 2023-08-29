using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenuBehaviour : MonoBehaviour
{
    public Slider sensitivitySlider;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject[] checkBoxes;
    public AudioManager audioManager;
    
    public void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity", -1f);
        SetCheckBoxes();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    public void CloseSettings()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }
    public void SetSensitivty()
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.value);
    }
    public void OnOffMusic()
    {
        PlayerPrefs.SetInt("Music", PlayerPrefs.GetInt("Music",1) == 1 ? 0 : 1);
        audioManager.SetMusic();
    }
    private void SetCheckBoxes()
    {
        checkBoxes[0].transform.GetChild(0).gameObject.SetActive(PlayerPrefs.GetInt("Music",1) == 1);
        checkBoxes[1].transform.GetChild(0).gameObject.SetActive(PlayerPrefs.GetInt("Sound",1) == 1);
    }
    public void OnOffSound()
    {
        PlayerPrefs.SetInt("Sound", PlayerPrefs.GetInt("Sound",1) == 1 ? 0 : 1);
    }
}

