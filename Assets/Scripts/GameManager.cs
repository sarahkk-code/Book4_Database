using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public AccessDB accessDB;

    // Add Score button
    public void AddScore()
    {
        StartCoroutine(SaveScoreAndContinue());
    }

    IEnumerator SaveScoreAndContinue()
    {
        // Start saving the score
        accessDB.UpdateScore();

        // Give the database request time to complete
        yield return new WaitForSeconds(1f);

        // Go to the database/high scores scene
        SceneManager.LoadScene("DatabaseScene");
    }

    // Go to Exit without adding a score
    public void GoToExit()
    {
        SceneManager.LoadScene("Exit");
    }
}