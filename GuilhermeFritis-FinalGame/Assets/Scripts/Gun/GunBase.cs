using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform shootPos;
    public float fireRate = 0.2f;
    public float projetileSpeed = 50f;

    private Coroutine _shootCoroutine;
    protected bool _activeWeapon;

    protected virtual IEnumerator ShootCoroutine()
    {     
        while(_activeWeapon)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }   
    }

    public virtual void Shoot()
    {
        var projectile = Instantiate(prefabProjectile, shootPos.position, shootPos.rotation);
        projectile.speed = projetileSpeed;
    }

    public void StartShoot()
    {
        _shootCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShoot()
    {
        if(_shootCoroutine != null)
        {
            StopCoroutine(_shootCoroutine);
        }
    }

    public virtual void SetActiveWeapon(bool active)
    {
        _activeWeapon = active;
    }
}
