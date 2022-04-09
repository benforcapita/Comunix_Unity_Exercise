using System;
using System.Collections.Generic;
using Core.References;
using Sirenix.OdinInspector;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameManager
{
/**
 * <summary>
 * This class is responsible for managing the game state.
 * It is managing lives and points of the player.
 * </summary>
 */
    public class GameState : MonoBehaviour
    {
        [SerializeField]private int score;
        [SerializeField] private int lives = 10;
         [SerializeField] private PlayerSpawner playerSpawner;
         [SerializeField] private List<GameObject> initialBombs;
         [SerializeField] private IntVariable scoreVariable;
         [SerializeField] private IntVariable livesVariable;
         [SerializeField] private GameObject gameOverPanel;
         [SerializeField] private string nextLevel;
         [SerializeField] private List<GameObject> bombs;
        [SerializeField] private SceneLoader loader;

         

         

         
         IDisposable bombHitSub = null;
         IDisposable OrcHitSub  = null;
         IDisposable bombAddedSub = null;



        private void OnEnable()
        {
            bombHitSub = MessageBroker.Default.Receive<BombHitEventArgs>().ObserveOnMainThread().Subscribe(AddScore);
            OrcHitSub = MessageBroker.Default.Receive<OrcHitEventArgs>().ObserveOnMainThread().Subscribe(LoseLife);
            bombAddedSub = MessageBroker.Default.Receive<InitBombEventArgs>().ObserveOnMainThread().Subscribe(args =>
            {
                if(bombs.Contains(args.bomb))
                    return;
                bombs.Add(args.bomb);
            });
            Time.timeScale = 0;
            score = scoreVariable.runtimeValue;
            lives = livesVariable.runtimeValue;
        }

      

        private void LoseLife(OrcHitEventArgs obj)
        {
            if(lives > 1)
            {
                lives--;
                livesVariable.runtimeValue = lives;
            }
            else
            {
                MessageBroker.Default.Publish(new GameOverEventArgs());
                gameOverPanel.SetActive(true);
            }
        
        }

        private void AddScore(BombHitEventArgs obj)
        {
            score += obj.ScoreToAdd;
            scoreVariable.runtimeValue = score;
           //remove null game Objects from list
            bombs.RemoveAll(x => x == null);
            Debug.Log( "Bombs left: " + bombs.Count);
            //check if there are no more bombs left
            if (bombs.Count != 0) return;
            Debug.Log("Complete Game");
            MessageBroker.Default.Publish(new LevelCompleteEventArgs());
            Time.timeScale = 1;
            loader.LoadScene(nextLevel);
        }
        [Button]
        public void StartGame() 
        {
            Time.timeScale = 1;
            playerSpawner.StartGame();
            foreach (var bomb in initialBombs)
            {
                bomb.SetActive(true);
            }
            MessageBroker.Default.Publish(new GameStartEventArgs());
        }

        private void OnDisable()
        {
            bombHitSub.Dispose();
            OrcHitSub.Dispose();
            bombAddedSub.Dispose();
        }
    }
}
