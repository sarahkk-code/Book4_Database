using UnityEngine;

public class DatabaseManager : MonoBehaviour
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
            Debug.LogError("AccessDB is not connected to DatabaseManager!");
        }
    }
}