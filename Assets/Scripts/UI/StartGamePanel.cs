using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class StartGamePanel : MonoBehaviour
{
    private void OnEnable()
    {
        MessageBroker.Default.Receive<GameStartEventArgs>().ObserveOnMainThread().Subscribe(args =>
        {
            this.gameObject.SetActive(false);
        });
    }
}