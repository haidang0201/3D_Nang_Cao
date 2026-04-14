using UnityEngine;

public class QuitButton : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("QUIT GAME"); // 👉 để test trong Unity

        Application.Quit(); // 👉 thoát game (chỉ hoạt động khi build)
    }
}