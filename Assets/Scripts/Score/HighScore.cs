using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    /**
     * This class is used to store the high score of the player.
     * I have decided to use a JSON file to store the high score inside the player prefs to avoid having to use the database.
     * I Believe That using a database for a single player game is an overkill.
     */
    public static void SaveHighScore(string name, int score)
    {
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            var saveObject = new Save();
            //Added this entry just to give people a score to beat.
            saveObject.Scores.Add("TryToBeatMe",100);
            saveObject.Scores.Add(name, score);
            var json = JsonConvert.SerializeObject(saveObject);
            PlayerPrefs.SetString("HighScore", json);
        }
        else
        {
            var saveObject = JsonConvert.DeserializeObject<Save>(PlayerPrefs.GetString("HighScore"));
            saveObject.Scores.Add(name, score);
            var json = JsonConvert.SerializeObject(saveObject);
            PlayerPrefs.SetString("HighScore", json);
        }
    }

    public static Dictionary<string, int> GetHighScore()
    {
        var output = new Dictionary<string, int>();
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            var saveObject = new Save();
            //Added this entry just to give people a score to beat.
            saveObject.Scores.Add("TryToBeatMe",100);
            var json = JsonConvert.SerializeObject(saveObject);
            PlayerPrefs.SetString("HighScore", json);
        }
        else
        {
            var saveObject = JsonConvert.DeserializeObject<Save>(PlayerPrefs.GetString("HighScore"));
            return saveObject.Scores;
        }

        return output;
    }
    public static void DeleteHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
}


[System.Serializable]
public class Save
{
    public Dictionary<string, int> Scores = new Dictionary<string, int>();
}

