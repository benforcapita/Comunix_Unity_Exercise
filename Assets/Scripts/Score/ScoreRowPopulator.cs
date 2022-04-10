using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreRowPopulator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private TextMeshProUGUI score;
    
    public void Populate(string name, int score)
    {
        this.name.text = name;
        this.score.text = score.ToString();
    }
    
}
