using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider collider;
        public FlashColor flashColor;
        public ParticleSystem hit_VFX;
        public ParticleSystem death_VFX;
        public float baseHealth = 10f;
        public bool destroyOnKill = false;
        [Header("Start Animation")]
        public float startAnimDuration = .2f;
        public Ease startAnimEase = Ease.OutBack;
        public bool startWithAnim = true;

        [SerializeField]
        private AnimationBase _animationBase;
        private float _curHealth;

        void OnValidate()
        {
            collider = gameObject.GetComponent<Collider>();
            flashColor = gameObject.GetComponent<FlashColor>();
        }

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
            PlayAnimationByType(AnimationType.DEATH);
            if(death_VFX != null)
            {
                death_VFX.Play();
            }
            OnDie();
        }

        protected virtual void OnDie()
        {
            if(collider != null)
            {
                collider.enabled = false;
            }

            if(destroyOnKill)
            {
                Destroy(gameObject, 2);
            }
        }

        protected virtual void OnDamage(float damage)
        {
            if(flashColor != null)
            {
                flashColor.Flash(Color.white);
            }
            if(hit_VFX != null)
            {
                hit_VFX.Emit(10);
            }

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

        public void PlayAnimationByType(AnimationType animationType)
        {
            _animationBase.PlayAnimationByType(animationType);
        }
        #endregion      

        public void TakeDamage(float damage)
        {
            OnDamage(damage);
        }
    }
}
