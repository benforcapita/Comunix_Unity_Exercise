using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameManager
{
    public class GameState : MonoBehaviour
    {
        [SerializeField]private int score;
        [SerializeField] private int lives = 10;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI livesText;
         [SerializeField] private GameObject player;
         [SerializeField] private List<GameObject> initialBombs;



        private void OnEnable()
        {
            MessageBroker.Default.Receive<BombHitEventArgs>().ObserveOnMainThread().Subscribe(AddScore);
            MessageBroker.Default.Receive<OrcHitEventArgs>().ObserveOnMainThread().Subscribe(LoseLife);
            Time.timeScale = 0;

        }

        private void LoseLife(OrcHitEventArgs obj)
        {
            if(lives > 0)
            {
                lives--;
                livesText.text = lives.ToString();
            }
            else
            {
                MessageBroker.Default.Publish(new GameOverEventArgs());
            }
        
        }

        private void AddScore(BombHitEventArgs obj)
        {
            score += obj.ScoreToAdd;
            scoreText.text = score.ToString();
        }
        [Button]
        public void StartGame() 
        {
            Time.timeScale = 1;
            player.SetActive(true);
            foreach (var bomb in initialBombs)
            {
                bomb.SetActive(true);
            }
            MessageBroker.Default.Publish(new GameStartEventArgs());
        }

    }
}
