using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class ManagingScenes : MonoBehaviour
{
    public int lvlValue;
    public void ExitGame()
    {
        Time.timeScale = 0;
        Application.Quit();
    }
    public void ReloadGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1;
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(lvlValue);
    }
}
