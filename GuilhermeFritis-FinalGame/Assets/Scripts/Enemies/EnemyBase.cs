using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;
using NaughtyAttributes;

namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable, IWakeableEnemy
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
        public bool lookAtPlayer = false;
        [ShowIf("lookAtPlayer")]
        public float lookRotation = 0.1f;
        public float damage = 1f;

        [SerializeField]
        private AnimationBase _animationBase;
        private float _curHealth;
        private GameObject player;

        void OnValidate()
        {
            collider = gameObject.GetComponent<Collider>();
            flashColor = gameObject.GetComponent<FlashColor>();
        }

        void Awake()
        {
            Init();
        }

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        public virtual void Update()
        {
            if(lookAtPlayer && player != null)
            {
                transform.rotation = Quaternion.Lerp(
                    transform.rotation, 
                    Quaternion.LookRotation(player.transform.position - transform.position),
                    lookRotation * Time.deltaTime
                );
            }
        }

        public virtual void WakeUp(GameObject player)
        {

        }

        public virtual void Sleep()
        {
            
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

        public void TakeDamage(float damage, Vector3 direction)
        {
            transform.DOMove(transform.position - direction * 0.5f, 0.1f);
            TakeDamage(damage);
        }

        void OnCollisionEnter(Collision collision)
        {
            var damageable = collision.gameObject.GetComponent<IDamageable>();
            if(damageable != null)
            {
                Vector3 dir = collision.transform.position - transform.position;
                dir = -dir.normalized;
                dir.y = 0f;
                damageable.TakeDamage(damage, dir);
            }
        }
    }
}
