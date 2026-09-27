using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class AccessDB : MonoBehaviour
{
    private string getScoresURL = "http://localhost/getScores.php";
    private string updateScoreURL = "http://localhost/updateScore.php";

    public TMP_InputField playerName;
    public TMP_InputField score;
    public TMP_Text highScores;

    // These are used by the Exit scene
    public TMP_Text namesText;
    public TMP_Text scoresText;

    private void Start()
    {
        GetScores();
    }

    // -----------------------------
    // GET HIGH SCORES
    // -----------------------------
    public void GetScores()
    {
        StartCoroutine(GetScoresCoroutine());
    }

    IEnumerator GetScoresCoroutine()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(getScoresURL))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string result = www.downloadHandler.text;

                Debug.Log("PHP Response: " + result);

                // Normal high-score display
                if (highScores != null)
                {
                    highScores.text = result;
                }

                // Clear the Exit scene text
                if (namesText != null)
                {
                    namesText.text = "";
                }

                if (scoresText != null)
                {
                    scoresText.text = "";
                }

                // Split database results into individual lines
                string[] rows = result.Split('\n');

                foreach (string row in rows)
                {
                    if (string.IsNullOrWhiteSpace(row))
                        continue;

                    string[] data = row.Trim().Split(',');

                    if (data.Length >= 2)
                    {
                        string name = data[0];
                        string scoreValue = data[1];

                        if (namesText != null)
                        {
                            namesText.text += name + "\n";
                        }

                        if (scoresText != null)
                        {
                            scoresText.text += scoreValue + "\n";
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Error getting scores: " + www.error);
                Debug.LogError("PHP Response: " + www.downloadHandler.text);
            }
        }
    }

    // -----------------------------
    // UPDATE PLAYER SCORE
    // -----------------------------
    public void UpdateScore()
    {
        if (playerName == null)
        {
            Debug.LogError("Player Name input is not connected!");
            return;
        }

        if (score == null)
        {
            Debug.LogError("Score input is not connected!");
            return;
        }

        string name = playerName.text.Trim();
        string scoreText = score.text.Trim();

        if (name == "")
        {
            Debug.LogError("Please enter a player name.");
            return;
        }

        if (scoreText == "")
        {
            Debug.LogError("Please enter a score.");
            return;
        }

        int scoreValue;

        if (!int.TryParse(scoreText, out scoreValue))
        {
            Debug.LogError("Score must be a number.");
            return;
        }

        StartCoroutine(UpdateScoreCoroutine(name, scoreValue));
    }

    // -----------------------------
    // SEND SCORE TO PHP / MYSQL
    // -----------------------------
    IEnumerator UpdateScoreCoroutine(string name, int scoreValue)
    {
        WWWForm form = new WWWForm();

        form.AddField("name", name);
        form.AddField("score", scoreValue);

        using (UnityWebRequest www = UnityWebRequest.Post(updateScoreURL, form))
        {
            yield return www.SendWebRequest();

            Debug.Log("HTTP Status: " + www.responseCode);
            Debug.Log("PHP Response: " + www.downloadHandler.text);

            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Score updated successfully!");

                GetScores();
            }
            else
            {
                Debug.LogError("Error updating score: " + www.error);
                Debug.LogError("PHP Response: " + www.downloadHandler.text);
            }
        }
    }
}