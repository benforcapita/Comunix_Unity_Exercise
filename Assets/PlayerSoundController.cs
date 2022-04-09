using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

/**
 * This class is responsible for the player Sound
 */
public class PlayerSoundController : MonoBehaviour
{
    [BoxGroup("WalkSound")] [SerializeField] private AudioSource walkAudioSource;
   [BoxGroup("WalkSound")][SerializeField] private AudioClip walkSound;
   [BoxGroup("WalkSound")] [SerializeField] private float speedWalkSound;
   [BoxGroup("AttackSound")] [SerializeField] private AudioSource AttackAudioSource;
   [BoxGroup("AttackSound")][SerializeField] private AudioClip attackSound;
   [BoxGroup("AttackSound")] [SerializeField] private float AttackSoundSpeed;
    
    private IDisposable walkSoundsubscription = null;
    private IDisposable attackSoundsubscription = null;
    
    private void OnEnable()
    {
        walkSoundsubscription =   MessageBroker.Default.Receive<HorizontalPlayerMoveEventArgs>().ObserveOnMainThread().Subscribe(WalkingSound);
        attackSoundsubscription =   MessageBroker.Default.Receive<PlayerAttackEventArgs>().ObserveOnMainThread().Subscribe(AttackSound);
       walkAudioSource.clip = walkSound;
       AttackAudioSource.clip = attackSound;
    }

    private void AttackSound(PlayerAttackEventArgs obj)
    {
        if(AttackAudioSource.isPlaying)
            return;
        if (obj.fireAxis > 0)
        {
            AttackAudioSource.Play();
            AttackAudioSource.pitch = speedWalkSound;
        }
        else
        { 
            AttackAudioSource.Pause();
        }
    }

    private void WalkingSound(HorizontalPlayerMoveEventArgs obj)
    {
        if (obj.AxisValue != 0)
        {
            if(walkAudioSource.isPlaying)
                return;
            walkAudioSource.loop = true;
            walkAudioSource.Play();
            walkAudioSource.pitch = speedWalkSound;
        }
        else
        { 
            walkAudioSource.loop = false;
            walkAudioSource.Pause();
        }
      
    }

    private void OnDisable()
    {
        walkSoundsubscription.Dispose();
        attackSoundsubscription.Dispose();
    }
}
