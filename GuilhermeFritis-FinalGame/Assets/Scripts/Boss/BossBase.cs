using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.StateMachine;
using DG.Tweening;
using Padrao.Core.Utils;

namespace Boss{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }

    [RequireComponent(typeof(HealthBase))]
    public class BossBase : MonoBehaviour, IWakeableEnemy
    {
        [Header("Animation")]
        public float startAnimDuration = .5f;
        public Ease startAnimEase = Ease.OutBack;
        [Header("Movements")]
        public float speed = 5f;
        public List<Transform> waypoints = new List<Transform>();
        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttacks = .5f;
        public HealthBase healthBase;
        [Header("Events")]
        public Action<BossBase> OnKill;

        private StateMachine<BossAction> stateMachine;

        void OnValidate()
        {
            healthBase = GetComponent<HealthBase>();
        }

        private void Awake() {
            Init();
            healthBase.OnDeath += BossDeath;
        }

        public void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.IDLE, new BossStateIdle());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());

            SwitchState(BossAction.INIT);
        }

        public virtual void WakeUp(GameObject player)
        {
            SwitchState(BossAction.WALK);
        }

        public virtual void Sleep()
        {
            SwitchState(BossAction.IDLE);
        }

        private void BossDeath(HealthBase hb)
        {
            SwitchState(BossAction.DEATH);
            OnKill?.Invoke(this);
        }

        #region WALK
        public void GoToRandomPoint(Action onArrive = null)
        {
            StartCoroutine(GoToPointCoroutine(waypoints.GetRandom(), onArrive));
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
            while(Vector3.Distance(transform.position, t.position) > 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();
        }
        #endregion

        #region ATTACK
        public void StartAttack(Action onEndAttack = null)
        {
            StartCoroutine(AttackCoroutine(onEndAttack));
        }

        IEnumerator AttackCoroutine(Action onEndAttack = null)
        {
            int attacks = 0;
            while(attacks < attackAmount)
            {
                attacks++;
                transform.DOScale(1.1f, .1f).SetLoops(2, LoopType.Yoyo);

                yield return new WaitForSeconds(timeBetweenAttacks);
            }
            onEndAttack?.Invoke();
        }
        #endregion

        #region STATE MACHINE
        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }
        #endregion

        #region ANIMATION
        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimDuration).SetEase(startAnimEase).From();
        }
        #endregion
    }
}