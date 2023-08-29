using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuButton : MonoBehaviour
{
    public GameObject mainMenuButton;
    public bool isMainMenuButtonShown = false;

    private void Start() {
        mainMenuButton.SetActive(false);
        StartCoroutine(ShowMainMenuButton(6f));
    }
    private IEnumerator ShowMainMenuButton(float time)
    {
        yield return new WaitForSeconds(time);
        mainMenuButton.SetActive(true);
        isMainMenuButtonShown = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void Update() {
        if(isMainMenuButtonShown && Input.GetKeyDown(KeyCode.Escape))
        {
            MainMenu();
        }
    }
}
