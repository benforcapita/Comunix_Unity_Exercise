using System;
using UniRx;
using UnityEngine;

namespace UI
{
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