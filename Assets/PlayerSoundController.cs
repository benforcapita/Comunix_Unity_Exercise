using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

/**
 * This class is responsible for the player Sound
 */
public class PlayerSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip walkSound;
    private IDisposable subscription = null;
    
    private void OnEnable()
    {
       subscription =   MessageBroker.Default.Receive<HorizontalPlayerMoveEventArgs>().ObserveOnMainThread().Subscribe(WalkingSound);
        audioSource.clip = walkSound;
    }

    private void WalkingSound(HorizontalPlayerMoveEventArgs obj)
    {
        if (obj.AxisValue != 0)
        {
            if(audioSource.isPlaying)
                return;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        { 
            audioSource.loop = false;
            audioSource.Pause();
        }
      
    }

    private void OnDisable()
    {
        subscription.Dispose();
    }
}
