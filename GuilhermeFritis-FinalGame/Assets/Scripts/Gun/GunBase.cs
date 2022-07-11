using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform shootPos;
    public float fireRate = 0.2f;
    public KeyCode shootKey = KeyCode.Mouse0;
    public float projetileSpeed = 50f;

    private Coroutine _shootCoroutine;

    protected virtual IEnumerator ShootCoroutine()
    {     
        while(true)
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
        _shootCoroutine =  StartCoroutine(nameof(ShootCoroutine));
    }

    public void StopShoot()
    {
        if(_shootCoroutine != null)
        {
            StopCoroutine(_shootCoroutine);
        }
    }
}
