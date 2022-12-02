using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(HealthBase))]
public class DestructableBase : MonoBehaviour
{
    public HealthBase health;
    public SOAnimation hitShake;


    void OnValidate()
    {
        if(health == null)
        {
            health = GetComponent<HealthBase>();
        }
    }

    void Start()
    {
        health.OnDamage += OnHit;
    }

    private void OnHit(HealthBase hp)
    {
        hitShake.DGAnimate(transform.DOShakeScale(hitShake.duration, Vector3.up/2, Mathf.RoundToInt(hitShake.value)));
    }
}
