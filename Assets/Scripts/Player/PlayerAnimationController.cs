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

        [SerializeField] private bool isWalking;
        

        private void OnEnable()
        {
            MessageBroker.Default.Receive<HorizontalPlayerMoveEventArgs>().ObserveOnMainThread().Subscribe(MoveHorizontal);
            MessageBroker.Default.Receive<PlayerAttackEventArgs>().ObserveOnMainThread().Subscribe(PlayerAttackAnimation);
        }

        private void PlayerAttackAnimation(PlayerAttackEventArgs obj)
        {
            animator.SetTrigger(IsAttacking);
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
    }
}