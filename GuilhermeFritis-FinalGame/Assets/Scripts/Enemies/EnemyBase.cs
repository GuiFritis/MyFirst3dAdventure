using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public float baseHealth = 10f;
        public bool destroyOnKill = false;

        private float _curHealth;

        protected virtual void Init()
        {
            ResetLife();
        }

        private void ResetLife()
        {
            _curHealth = baseHealth;
        }

        protected virtual void Die()
        {
            OnDie();
        }

        protected virtual void OnDie()
        {
            if(destroyOnKill)
            {
                Destroy(gameObject);
            }
        }

        public virtual void TakeDamage(float damage)
        {
            _curHealth -= damage;
            if(_curHealth <= 0)
            {
                Die();
            }
        }
    }
}
