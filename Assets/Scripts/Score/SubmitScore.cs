using System.Collections;
using System.Collections.Generic;
using Core.References;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class SubmitScore : MonoBehaviour
{
  [SerializeField] private SceneLoader sceneLoader;
  [SerializeField] private TMP_InputField tmpInputField;
  [SerializeField] private IntVariable score;
  [Button]
  public void Submit()
  {
    if (tmpInputField.text.Length > 0)
    {
      HighScore.SaveHighScore(tmpInputField.text, score.runtimeValue);
      sceneLoader.LoadScene("MainMenu");
    }
    else
    {
      sceneLoader.LoadScene("MainMenu");
    }
  }
  
    
}
