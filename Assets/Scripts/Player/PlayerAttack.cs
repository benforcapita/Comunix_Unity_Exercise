using System;
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
        }
        //Invoke OnAttack When Player Is Shooting
        private void OnAttack()
        {
            if (attack != null)
                attack.Attack(attackPoint, attackRange);
        }
        
        private void Update()
        {
            IsShooting = Input.GetAxisRaw("Fire1") >0;
        }
    }
}