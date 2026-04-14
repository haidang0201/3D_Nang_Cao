using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainButton : MonoBehaviour
{
    [SerializeField] string sceneName = "Main"; // 👉 scene game chính

    public void PlayAgain()
    {
        Time.timeScale = 1f; // tránh bị pause
        SceneManager.LoadScene(sceneName);
    }
}