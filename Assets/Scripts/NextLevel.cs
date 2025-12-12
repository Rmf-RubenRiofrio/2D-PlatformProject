using System.Diagnostics.Contracts;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public string LevelName;
    public int nextLevelValue;
    public void LoadNextLevel()
    {
        PlayerPrefs.SetInt("LevelReached", nextLevelValue);
        Time.timeScale = 1f; // Resume the game
        UnityEngine.SceneManagement.SceneManager.LoadScene(LevelName);

    }
}
