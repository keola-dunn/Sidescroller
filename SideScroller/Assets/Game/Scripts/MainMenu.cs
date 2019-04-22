using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static int lastLevelPlayed = 0;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(lastLevelPlayed);
    }

    public void LoadLevel(int level)
    {
        lastLevelPlayed = level;
        SceneManager.LoadScene(level);
    }
}
