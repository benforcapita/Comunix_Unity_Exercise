using UniRx;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        //animator
        [SerializeField] private Animator animator;
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
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
            Debug.Log(obj.AxisValue + ":" + (obj.AxisValue!=0));
            isWalking = (obj.AxisValue != 0);
            animator.SetBool(IsWalking, obj.AxisValue != 0);
        }
    }
}