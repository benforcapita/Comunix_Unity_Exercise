using System;
using UniRx;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        //animator
        [SerializeField] private Animator animator;
        private static readonly int IsWalking = Animator.StringToHash("speed");
        private static readonly int IsAttacking = Animator.StringToHash("attack");
        private static readonly int IsAttackingSpecial = Animator.StringToHash("special");
        private static readonly float AttackSpeed = 1.0f;

        private bool canTriggerAttackAnimation = true;

        [SerializeField] private bool isWalking;
        private IDisposable HorizontalMovementSub = null;
        private IDisposable VerticalMovementSub = null;
        

        private void OnEnable()
        {
           HorizontalMovementSub =  MessageBroker.Default.Receive<HorizontalPlayerMoveEventArgs>().ObserveOnMainThread().Subscribe(MoveHorizontal);
            VerticalMovementSub = MessageBroker.Default.Receive<PlayerAttackEventArgs>().ObserveOnMainThread().Subscribe(PlayerAttackAnimation);
        }

        private void PlayerAttackAnimation(PlayerAttackEventArgs obj)
        {
            //Prevent animation for spammed
            if (!(obj.fireAxis > 0)) return;
            if (!canTriggerAttackAnimation) return;
            Observable.Timer(TimeSpan.FromSeconds(AttackSpeed)).Subscribe(_ => canTriggerAttackAnimation = true);
            animator.SetTrigger(IsAttacking);
            canTriggerAttackAnimation = false;
        }


        private void MoveHorizontal(HorizontalPlayerMoveEventArgs obj)
        {
            isWalking = (obj.AxisValue != 0);
            if (isWalking)
            {
                animator.SetFloat(IsWalking, Math.Abs(obj.AxisValue));
            }
            else
            {
                animator.SetFloat(IsWalking, 0);
            }
        }

        private void OnDisable()
        {
            HorizontalMovementSub.Dispose();
            VerticalMovementSub.Dispose();
        }
    }
}