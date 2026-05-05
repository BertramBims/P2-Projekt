using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject levelSelectUI;
    public GameObject MainMenuUI;
    private bool isPaused = false;

    // PAUSE

    private void Start()
    {
        if (levelSelectUI != null)
            levelSelectUI.SetActive(false);
    }

    void Update()
{
    if (Keyboard.current == null) return;

    if (Keyboard.current.escapeKey.wasPressedThisFrame)
    {
        if (levelSelectUI != null && levelSelectUI.activeSelf)

        {
            CloseLevelSelect();
            BackToMain();
            return;
        }

        if (pauseUI != null)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }
}

    public void Pause()
    {
        if (pauseUI == null) return;

        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume()
    {
        if (pauseUI == null) return;

        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    // MENU

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SERHAT TEST");
    }

    public void OpenLevelSelect()
    {
        if (levelSelectUI != null)
            levelSelectUI.SetActive(true);

        if (MainMenuUI != null)
            MainMenuUI.SetActive(false);
    }

    public void BackToMain()
    {
        if (MainMenuUI != null)
            MainMenuUI.SetActive(true);
    }

    public void CloseLevelSelect()
    {
        if (levelSelectUI != null)
            levelSelectUI.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
