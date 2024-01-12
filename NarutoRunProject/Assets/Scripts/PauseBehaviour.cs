using UnityEngine;

public class PauseBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;

    private GameObject lastMenu;

    private void Start()
    {
        gameUI.SetActive(true);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        lastMenu = gameUI;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !LevelManager.instance.finishedLevel)
        {
            ReturnMenu();
        }
    }

    public void ReturnMenu()
    {
        if (lastMenu == gameUI)
        {
            ChangeMenu(pauseMenu);
            Time.timeScale = 0f;
        }
        else if (lastMenu == pauseMenu)
        {
            ChangeMenu(gameUI);
            Time.timeScale = 1f;
        }
        else if (lastMenu == settingsMenu)
        {
            ChangeMenu(pauseMenu);
        }
    }

    private void ChangeMenu(GameObject menu)
    {
        lastMenu.SetActive(false);
        menu.SetActive(true);
        lastMenu = menu;
    }
}