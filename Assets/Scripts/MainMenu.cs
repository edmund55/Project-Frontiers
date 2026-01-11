using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start
    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    // How To Play
    public void OpenHowToPlay()
    {
        SceneManager.LoadScene("HowtoPlayScene");
    }

    // Back to Main Menu from How To Play
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    // Quit
    public void QuitGame()
    {
        Application.Quit();
    }
}
