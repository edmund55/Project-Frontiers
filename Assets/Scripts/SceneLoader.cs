using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    // How To Play (Controls)
    public void OpenControlsScene()
    {
        SceneManager.LoadScene("ControlsScene1");
    }

    // Settings
    public void OpenSettingsScene()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    // Back to Main Menu from How To Play
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    // Next Scene (ButtonNext)
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1
        );
    }

    // Previous Scene (ButtonBack)
    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1
        );
    }

    // Quit
    public void QuitGame()
    {
        Application.Quit();
    }
}
