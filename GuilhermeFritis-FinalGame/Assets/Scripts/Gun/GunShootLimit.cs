using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{    
    public List<UIFillUpdater> uIGunUpdaters;
    public int maxShoot = 5;
    public float cooldown = 1f;
    
    private int _curShots = 0;

    void Awake()
    {
        GetAllUIs();
    }

    protected override IEnumerator ShootCoroutine()
    {
        while(_curShots < maxShoot && _activeWeapon)
        {
            Shoot();
            _curShots++;
            CheckAmmo();
            UpdateUI();
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
            if(_activeWeapon)
            {
                foreach (var item in uIGunUpdaters)
                {
                    item.UpdateValue(time/cooldown);
                }
            }
            yield return new WaitForEndOfFrame();
        }
        _curShots = 0;
    }

    public void ForceRecharge()
    {
        _curShots = maxShoot;
        CheckAmmo();
    }

    private void UpdateUI()
    {
        if(_activeWeapon)
        {
            foreach (var item in uIGunUpdaters)
            {
                item.UpdateValue(maxShoot, _curShots);
            }
        }
    }

    private void GetAllUIs()
    {
        var listOfUis = GameObject.FindGameObjectsWithTag("GunUI"); //FindObjectsOfType<UIFillUpdater>().ToList();
        foreach (var item in listOfUis)
        {
            uIGunUpdaters.Add(item.GetComponent<UIFillUpdater>());
        }
    }

    public override void SetActiveWeapon(bool active)
    {
        base.SetActiveWeapon(active);
        if(active)
        {
            UpdateUI();
        }
    }
}
