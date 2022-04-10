using System.Collections;
using System.Collections.Generic;
using Core.References;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static readonly List<string> SceneNames = new List<string>()
    {
        "MainMenu", "LevelOne","LevelTwo","LevelThree","ScoreScene","EndScene"
    };

    [SerializeField] private bool isARestart;

    [ShowIf("isARestart")][SerializeField]
    private List<IntVariable> variablesToReset = new List<IntVariable>();


    public void LoadScene(string sceneName)
    {
        if (isARestart)
        {
            foreach (ISerializationCallbackReceiver serializationCallbackReceiver in variablesToReset)
            {
             serializationCallbackReceiver.OnAfterDeserialize();   
            }
        }
        if (SceneNames.Contains(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
