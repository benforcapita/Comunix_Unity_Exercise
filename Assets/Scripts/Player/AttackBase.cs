using System;
using System.Collections.Generic;
using Player.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    [Serializable]
    public abstract class AttackBase : MonoBehaviour, IAttack
    {
      [SerializeField] protected GameObject bulletPrefab;
      [SerializeField] protected int bulletPoolSize = 10;
      [SerializeField] protected List<GameObject> bulletPool;
        private void OnEnable()
        {
            for (int i = 0; i < bulletPoolSize; i++)
            {
                var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                bullet.SetActive(false);
                bullet.transform.SetParent(transform);
                bullet.transform.localPosition = Vector3.zero;
                bullet.transform.localRotation = Quaternion.identity;
                bulletPool.Add(bullet);
            }
        }
        public abstract void Attack(Transform attackPoint, float attackRange);
    }
}

