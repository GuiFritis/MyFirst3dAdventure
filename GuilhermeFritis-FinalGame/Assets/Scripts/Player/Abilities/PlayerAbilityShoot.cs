using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public List<GunBase> gunBases;
    public Transform gunPos;

    private int _curGun;
    private List<GunBase> _instantiatedGuns = new List<GunBase>();

    protected override void Init()
    {
        base.Init();

        CreateGuns();
        
        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
        inputs.Gameplay.Slot1.performed += ctx => ChangeWeapon(0);
        inputs.Gameplay.Slot2.performed += ctx => ChangeWeapon(1);
        inputs.Gameplay.RotateWeapon.performed += ctx => RotateWeapon(Mathf.Clamp(ctx.ReadValue<float>(), -1, 1));
    }

    private void CreateGuns()
    {
        foreach (var item in gunBases)
        {            
            _instantiatedGuns.Add(Instantiate(item, gunPos.position, gunPos.rotation, gunPos));
            _instantiatedGuns[_instantiatedGuns.Count-1].SetActiveWeapon(false);
        }
        ChangeWeapon(0);
    }

    private void StartShoot()
    {
        _instantiatedGuns[_curGun].StartShoot();
    }

    private void CancelShoot()
    {
        _instantiatedGuns[_curGun].StopShoot();
    }

    private void ChangeWeapon(int index)
    {
        _instantiatedGuns[_curGun].SetActiveWeapon(false);
        if(_instantiatedGuns[_curGun] is GunShootLimit shootLimit)
        {
            shootLimit.ForceRecharge();
        }
        _curGun = index;
        _instantiatedGuns[_curGun].SetActiveWeapon(true);
    }

    private void RotateWeapon(float value)
    {
        Debug.Log(value);
        int finalValue = _curGun + Mathf.RoundToInt(value);
        if(finalValue >= _instantiatedGuns.Count)
        {
            finalValue = 0;
        } 
        else if(finalValue < 0)
        {
            finalValue =  _instantiatedGuns.Count - 1;
        } 

        ChangeWeapon(finalValue);
    }
}
