using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class ScoreTable : MonoBehaviour
{
  [SerializeField] private GameObject tableRowPrefab;
  [SerializeField] private Transform tableContent;
  private void OnEnable()
  {
    PrintScores();
    CreateTable();
   
  }
  [Button]
  public void AddHighScore(string name, int score)
  {
    HighScore.SaveHighScore(name, score);
  }
[Button]
  public void PrintScores()
  {
    var scores = HighScore.GetHighScore();
    foreach (var score in scores)
    {
      Debug.Log("Name: " + score.Key + " Score: " + score.Value);
    }
  }
[Button]
  public void CreateTable()
  {
    var scores = HighScore.GetHighScore();
    //scores sort by value descending
    foreach (var score in scores.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value))
    {
      var row = Instantiate(tableRowPrefab, tableContent);
      row.GetComponent<ScoreRowPopulator>().Populate(score.Key, score.Value);
    }
  }
  [Button]
  public void ClearTable()
  {
    foreach (Transform child in tableContent)
    {
      if(Application.isEditor)  
        DestroyImmediate(child.gameObject);
      else Destroy(child.gameObject);
    }
  }
  [Button]
 public void DeleteAllScores()
  {
    HighScore.DeleteHighScore();
  }
}
