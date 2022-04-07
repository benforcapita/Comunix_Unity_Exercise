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
       protected Stack<GameObject> bulletPoolOne = new Stack<GameObject>();
      protected Stack<GameObject> bulletPoolTwo = new Stack<GameObject>();
      protected bool useFirstPool = true;
      [SerializeField] protected float attackSpeed;
      
        private void OnEnable()
        {
            for (var i = 0; i < bulletPoolSize; i++)
            {
                var position = transform.position;
                var bulletPosition = new Vector3(position.x, position.y*2, position.z);
                var bullet = Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
                bullet.SetActive(false);
                bullet.transform.SetParent(transform);
                bullet.transform.localPosition = Vector3.zero;
                bullet.transform.localRotation = Quaternion.identity;
                bulletPoolOne.Push(bullet);
            }
        }
        /*
         * Get bullet from pool and activate it 
         */
        protected GameObject GetBullet()
        {
          
            var usedBulletPool = useFirstPool ? bulletPoolOne : bulletPoolTwo;
            var unusedBulletPool = useFirstPool ? bulletPoolTwo : bulletPoolOne;
            //if first pool is empty, use second pool
            if (usedBulletPool.Count == 0)
            {
                useFirstPool = !useFirstPool;
                usedBulletPool = useFirstPool ? bulletPoolOne : bulletPoolTwo;
                unusedBulletPool = useFirstPool ? bulletPoolTwo : bulletPoolOne;
            }
            var bullet = usedBulletPool.Pop();
            bullet.SetActive(true);
            unusedBulletPool.Push(bullet);
            return bullet;
        }
        public abstract void Attack(Transform attackPoint, float attackRange);
    }
}

