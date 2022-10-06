using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnDeath;
    public bool destroyOnDeath = false;
    public float baseHealth = 10f;

    private float _curHealth;
    private bool dead = false;

    void Awake()
    {
        Init();
    }

    public void Init(){
        ResetLife();
    }

    protected void ResetLife()
    {
        _curHealth = baseHealth;
    }

    protected virtual void Death()
    {
        dead = true;
        OnDeath?.Invoke(this);
        if(destroyOnDeath)
        {
            Destroy(gameObject, 1f);
        }
    }

    public void TakeDamage(float damage)
    {
        _curHealth -= damage;
        OnDamage?.Invoke(this);
        if(_curHealth <= 0 && !dead)
        {
            Death();
        }
    }

    public void TakeDamage(float damage, Vector3 dir)
    {
        TakeDamage(damage);
    }
}
