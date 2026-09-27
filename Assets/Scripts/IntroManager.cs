using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    // Play button on the Intro screen
    public void PlayGame()
    {
        SceneManager.LoadScene("ShowScores");
    }

    // High Scores button on the Intro screen
    public void HighScores()
    {
        SceneManager.LoadScene("ShowScores");
    }
}