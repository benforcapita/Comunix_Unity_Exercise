using System;
using UniRx;
using UnityEngine;

namespace UI
{
    /*
     * Disabling the UI when the game starts
     */
    public class StartGamePanel : MonoBehaviour
    {
      private IDisposable _startGameSub;

        private void OnEnable()
        {
            _startGameSub = MessageBroker.Default.Receive<GameStartEventArgs>().ObserveOnMainThread().Subscribe(args =>
            {
                this.gameObject.SetActive(false);
            });
        }

        private void OnDisable()
        {
            _startGameSub.Dispose();
        }
    }
}