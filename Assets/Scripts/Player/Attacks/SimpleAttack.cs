using System;
using UnityEngine;

namespace Player.Attacks
{
    [Serializable]
    public class SimpleAttack : AttackBase
    {
        public override void Attack(Transform attackPoint, float attackRange)
        {
            Debug.Log("Attack");
            //instantiate attack prefab
            var position = attackPoint.position;
            var bullet = bulletPool[bulletPool.Count - 1];
            bullet.SetActive(true);

        }
    }
}
