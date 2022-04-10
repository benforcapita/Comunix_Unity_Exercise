using System;
using Core.References;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Mobile
{
    public class MobileControllers : MonoBehaviour
    {
        [SerializeField] private bl_Joystick Joystick;
        [SerializeField] private Button attackButton;
        
            // idisposible called start game subscription
            private IDisposable _startGameSubscription = null;

        private void OnEnable()
        {

           _startGameSubscription  =  MessageBroker.Default.Receive<GameStartEventArgs>().ObserveOnMainThread().Subscribe(GameStart);
        }

        private void GameStart(GameStartEventArgs obj)
        {
            if (Application.platform != RuntimePlatform.Android) return;
            Joystick.gameObject.SetActive(true);
            attackButton.gameObject.SetActive(true);
            MessageBroker.Default.Publish(new MobileControllersEventArgs(Joystick, attackButton));
        }

        private void OnDisable()
        {
            _startGameSubscription.Dispose();
           
        }
    }

    public class MobileControllersEventArgs
    {
        public bl_Joystick Joystick { get; }
        public Button AttackButton { get; }

        public MobileControllersEventArgs(bl_Joystick joystick, Button attackButton)
        {
            Joystick = joystick;
            AttackButton = attackButton;
        }
    }
}
