using System;
using GameManager;
using UniRx;
using UnityEngine;

namespace Player
{
    public class PlayerHitBox : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            MessageBroker.Default.Publish(new OrcHitEventArgs());
        }
    }
}
