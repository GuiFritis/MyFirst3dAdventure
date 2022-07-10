using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public int maxShoot = 5;
    public float cooldown = 1f;
    
    private int _curShots = 0;

    protected override IEnumerator ShootCoroutine()
    {
        while(_curShots < maxShoot)
        {
            Shoot();
            _curShots++;
            CheckAmmo();
            yield return new WaitForSeconds(fireRate);
        } 
    }

    private void CheckAmmo()
    {
        if(_curShots >= maxShoot)
        {
            StopShoot();
            StartRecharge();
        }
    }
    private void StartRecharge()
    {
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0f;
        while(time < cooldown)
        {
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _curShots = 0;
    }
}
