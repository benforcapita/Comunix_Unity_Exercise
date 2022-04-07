using System;
using UnityEngine;

namespace Player.Attacks
{
    [Serializable]
    public class SimpleAttack : AttackBase
    {
        public override void Attack(Transform attackPoint, float attackRange)
        {
            //instantiate attack prefab
            var position = attackPoint.position;
            var hit = Physics2D.Raycast(position, attackPoint.up, attackRange);
            if (hit.collider != null)
            {
            }
        }
    }
}
