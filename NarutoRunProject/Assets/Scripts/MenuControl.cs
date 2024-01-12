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
        if (LoadScreenPanel == null) { return; }
        
        LoadScreenPanel.SetActive(false);   
    }

    public void LoadScene(string name)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(name);
    }

    public void LoadWithProgressBar(string sceneName)
    {
        Time.timeScale = 1.0f;
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string SceneName)
    {
        AsyncOperation loader = SceneManager.LoadSceneAsync(SceneName, LoadSceneMode.Single);

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
        Time.timeScale = 1.0f;
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}