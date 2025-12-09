using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public string LevelName;
    public void LoadNextLevel()
    {
        Time.timeScale = 1f; // Resume the game
        UnityEngine.SceneManagement.SceneManager.LoadScene(LevelName);

    }
}
