using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{

    public GunBase gunBase;

    protected override void Init()
    {
        base.Init();
        
        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
    }

    private void StartShoot()
    {
        gunBase.StartShoot();
    }

    private void CancelShoot()
    {
        gunBase.StopShoot();
    }
}
