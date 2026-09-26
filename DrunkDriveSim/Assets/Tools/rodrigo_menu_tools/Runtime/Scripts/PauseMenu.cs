using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace MenuTemplates
{
}
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseFirstButton;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject optionsFirstButton;

    private bool isPaused = false;
    private bool inOptions = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Open pause menu
        if (!isPaused && !inOptions && (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame ||
                                        Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame))
        {
            Pause();
        }
        // Resume from pause menu
        else if (isPaused && !inOptions && (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame ||
                                            Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame ||
                                            Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame))
        {
            Resume();
        }
        // Go back from options to pause menu
        else if (inOptions && (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame ||
                               Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame))
        {
            OnBackButtonClick();
        }
    }

    public void Pause()
    {
        isPaused = true;
        inOptions = false;
        Time.timeScale = 0f;
        if (pausePanel != null)
            pausePanel.SetActive(true);
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        if (pauseFirstButton != null)
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    public void Resume()
    {
        isPaused = false;
        inOptions = false;
        Time.timeScale = 1f;
        if (pausePanel != null)
            pausePanel.SetActive(false);
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void onOptionsButtonClick()
    {
        inOptions = true;
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        if (optionsFirstButton != null)
            EventSystem.current.SetSelectedGameObject(optionsFirstButton);
    }
    public void OnFullscreenToggleChanged(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void OnVolumeSliderChanged(float value)
    {
        AudioListener.volume = value;
    }

    public void OnBackButtonClick()
    {
        inOptions = false;
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        if (pauseFirstButton != null)
            EventSystem.current.SetSelectedGameObject(pauseFirstButton);
    }

    public void OnQuitButtonClick()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
