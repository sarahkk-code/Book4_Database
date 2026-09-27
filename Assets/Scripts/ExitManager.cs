using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    // Play Again button
    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }

    // Quit button
    public void QuitGame()
    {
        Debug.Log("Quit button pressed.");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}