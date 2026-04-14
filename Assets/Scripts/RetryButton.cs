using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    [SerializeField] string sceneName = "Main";

    public void Retry()
    {
        Time.timeScale = 1f; // tránh bị pause
        SceneManager.LoadScene(sceneName);
    }
}           