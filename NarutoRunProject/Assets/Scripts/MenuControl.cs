using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    public static MenuControl instance;
    public GameObject LoadScreenPanel;
    public TextMeshProUGUI ProgressPercentage;
    public Slider ProgressBar;

    private void Awake()
    {
        instance = this;
        if(LoadScreenPanel != null)
        {
            LoadScreenPanel.SetActive(false);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void Controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void WinGame()
    {
        SceneManager.LoadScene("Win");
    }

    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene("Level" + levelNumber);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadWithProgressBar(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string SceneName)
    {
        AsyncOperation loader = SceneManager.LoadSceneAsync(SceneName);

        LoadScreenPanel.SetActive(true);
        while(!loader.isDone)
        {
            //change the value so it goes from 0 to 1 instead of 0 to 0.9f
            float progress = Mathf.Clamp01(loader.progress / 0.9f);
            ProgressBar.value = progress;
            //change the value so it goes from 0 to 100 with 2 decimal places
            progress = Mathf.Round(progress * 10000) / 100;
            ProgressPercentage.text = progress + "%";
            yield return null;
        }
    }


    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
