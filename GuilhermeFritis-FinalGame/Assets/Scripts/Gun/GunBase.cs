using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform shootPos;
    public float fireRate = 0.2f;
    public KeyCode shootKey = KeyCode.Mouse0;
    private Coroutine _shootCoroutine;

    void Update()
    {
        if(Input.GetKeyDown(shootKey))
        {
            _shootCoroutine =  StartCoroutine(nameof(StartShoot));
        } 
        else if(Input.GetKeyUp(shootKey) && _shootCoroutine != null)
        {
            StopCoroutine(_shootCoroutine);
        }
    }

    IEnumerator StartShoot()
    {     
        while(true)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }   
    }

    public void Shoot()
    {
        var projectile = Instantiate(prefabProjectile, shootPos.position, shootPos.rotation);
    }
}
