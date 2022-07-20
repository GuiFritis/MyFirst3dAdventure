using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour
    {
        public float baseHealth = 10f;
        public bool destroyOnKill = false;
        [Header("Start Animation")]
        public float startAnimDuration = .2f;
        public Ease startAnimEase = Ease.OutBack;
        public bool startWithAnim = true;

        private float _curHealth;

        void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            ResetLife();
            if(startWithAnim)
            {
                BornAnimation();
            }
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

        #region ANIMATION
        private void BornAnimation()
        {
            transform.DOScale(0, startAnimDuration).SetEase(startAnimEase).From();
        }
        #endregion
    }
}
