using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static readonly List<string> SceneNames = new List<string>()
    {
        "MainMenu", "LevelOne"
    };
    
    public void LoadScene(string sceneName)
    {
        if (SceneNames.Contains(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
