using System;
using UnityEngine;

namespace Player.Interfaces
{
    
    public interface IAttack 
    {
       void Attack(Transform attackPoint, float attackRange);
    }
}
