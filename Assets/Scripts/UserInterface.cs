using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject levelSelectUI;
    public GameObject mainMenuUI;
    private bool isPaused = false;

    private void Start()
    {
        if (levelSelectUI != null)
            levelSelectUI.SetActive(false);

        AddButtonClickSound(mainMenuUI);
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

    // PAUSE

    public void Pause()
    {
        if (pauseUI == null) return;

        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        AddButtonClickSound(pauseUI);
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
        {
            levelSelectUI.SetActive(true);
            AddButtonClickSound(levelSelectUI);
        }

        if (mainMenuUI != null)
            mainMenuUI.SetActive(false);
    }

    public void BackToMain()
    {
        if (mainMenuUI != null)
            mainMenuUI.SetActive(true);

        if (levelSelectUI != null)
            levelSelectUI.SetActive(false);
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

    // BUTTONSOUND

    void PlayButtonSound()
    {
        SoundManager.instance.PlaySound(Soundtype.Button);
    }

    void AddButtonClickSound(GameObject ui)
    {
        if (ui == null) return;

        Button[] buttons = ui.GetComponentsInChildren<Button>(true);

        foreach (Button btn in buttons)
        {
            btn.onClick.AddListener(PlayButtonSound);
        }
    }
}
    