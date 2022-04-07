using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Attacks
{
    [Serializable]
    public class SimpleAttack : AttackBase
    {
        public override void Attack(Transform attackPoint, float attackRange)
        {
            var position = attackPoint.position;
            var bullet = GetBullet();
            bullet.transform.position = position;
            bullet.transform.rotation = attackPoint.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>().Shoot(position, attackSpeed);
        }
    }
}
