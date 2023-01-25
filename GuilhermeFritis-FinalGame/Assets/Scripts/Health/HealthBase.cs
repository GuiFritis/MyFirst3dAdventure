using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnDeath;
    public bool destroyOnDeath = false;
    public float baseHealth = 10f;
    public List<UIFillUpdater> uiHealthUpdaters;
    public float damageMultiplier = 1f;

    private float _curHealth;
    private bool dead = false;

    void Awake()
    {
        Init();
    }

    public void Init(){
        ResetLife();
    }

    public void ResetLife()
    {
        ResetLife(baseHealth);
    }

    public void ResetLife(float life)
    {
        _curHealth = life;
        dead = false;
        UpdateUI();
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
        _curHealth -= damage * damageMultiplier;
        UpdateUI();
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

    public float GetCurHealth(){
        return _curHealth;
    }

    public void UpdateUI()
    {
        if(uiHealthUpdaters.Count > 0)
        {
            foreach (var item in uiHealthUpdaters)
            {
                item.UpdateValue(_curHealth/baseHealth);
            }
        }
    }

    public void ResetResistance()
    {
        damageMultiplier = 1f;
    }

    public void IncreaseResistance(float damageResistance, float duration)
    {
        StartCoroutine(IncreaseResistanceCoroutine(damageResistance, duration));
    }

    private IEnumerator IncreaseResistanceCoroutine(float damageResistance, float duration)
    {
        damageMultiplier += damageResistance;
        yield return new WaitForSeconds(duration);
        damageMultiplier -= damageResistance;
    }
}
