using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gunBase;
    public Transform gunPos;

    private GunBase _curGun;

    protected override void Init()
    {
        base.Init();

        CreateGun();
        
        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
    }

    private void CreateGun()
    {
        _curGun = Instantiate(gunBase, gunPos.position, gunPos.rotation, gunPos);
    }

    private void StartShoot()
    {
        _curGun.StartShoot();
    }

    private void CancelShoot()
    {
        _curGun.StopShoot();
    }
}
