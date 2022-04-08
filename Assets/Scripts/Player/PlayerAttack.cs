using System;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private AttackBase attack;
        [SerializeField] private Transform attackPoint;
        [SerializeField] private float attackRange;
        [SerializeField] private bool isShooting = false;
        private IDisposable fireSubscription;
        private bool IsShooting {
            set
            {
                if (isShooting && value == false)
                {
                    _onAttack.Invoke();
                }

                isShooting = value;
            }
            get => isShooting;
        }
        private UnityAction _onAttack;

        private void OnEnable()
        {
            _onAttack = OnAttack;
            fireSubscription = MessageBroker.Default.Receive<PlayerAttackEventArgs>().ObserveOnMainThread().Subscribe(args =>
            {
                IsShooting = args.fireAxis > 0;
            });
        }
        //Invoke OnAttack When Player Is Shooting
        private void OnAttack()
        {
            if (attack != null)
                attack.Attack(attackPoint, attackRange);
        }

        private void OnDisable()
        {
            fireSubscription.Dispose();
        }
    }
}