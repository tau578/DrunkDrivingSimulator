using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem; // Import the new Input System namespace
using UnityEngine.InputSystem.Controls; // Add this using directive

namespace MenuTemplates
{
}
public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject pressStartPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private GameObject MainMenuFirstButton;
    [SerializeField] private GameObject MainMenuSecondButton;
    [SerializeField] private GameObject MainMenuThirdButton;
    [SerializeField] private GameObject OptionsFirstButton;
    [SerializeField] private GameObject OptionsClosedButton;
    [SerializeField] private GameObject CreditsFirstButton;
    [SerializeField] private GameObject CreditsClosedButton;

    private bool hasStarted = false;

    private void Start()
    {
        pressStartPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        hasStarted = false;

        // Initialize toggle and slider values
        if (fullscreenToggle != null)
            fullscreenToggle.isOn = Screen.fullScreen;

        if (volumeSlider != null)
            volumeSlider.value = AudioListener.volume;
    }

    private void Update()
    {
        if (!hasStarted && IsAnyInput())
        {
            pressStartPanel.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(MainMenuFirstButton);
            mainMenuPanel.SetActive(true);
            hasStarted = true;
        }
    }

    // Use the new Input System to check for any input
    private bool IsAnyInput()
    {
        // Keyboard
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
            return true;

        // Mouse
        if (Mouse.current != null && (Mouse.current.leftButton.wasPressedThisFrame ||
                                      Mouse.current.rightButton.wasPressedThisFrame ||
                                      Mouse.current.middleButton.wasPressedThisFrame))
            return true;

        // Gamepad
        if (Gamepad.current != null && Gamepad.current.allControls.Any(c => c is ButtonControl btn && btn.wasPressedThisFrame))
            return true;

        return false;
    }

    public void onPlayButtonClick()
    {
        int currentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneBuildIndex + 1);
    }

    public void onOptionsButtonClick()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(OptionsFirstButton);
        mainMenuPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void onCreditsButtonClick()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(CreditsFirstButton);
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void onCloseOptionsButtonClick()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(MainMenuSecondButton);
        optionsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void onCloseCreditsButtonClick()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(MainMenuThirdButton);
        creditsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void onQuitButtonClick()
    {
        Application.Quit();
    }

    // Call this from the Toggle's OnValueChanged event
    public void OnFullscreenToggleChanged(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    // Call this from the Slider's OnValueChanged event
    public void OnVolumeSliderChanged(float value)
    {
        AudioListener.volume = value;
    }
}
