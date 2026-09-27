using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowScoresManager : MonoBehaviour
{
    public AccessDB accessDB;

    private void Start()
    {
        if (accessDB != null)
        {
            accessDB.GetScores();
        }
        else
        {
            Debug.LogError("AccessDB is not connected to ShowScoresManager!");
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
}