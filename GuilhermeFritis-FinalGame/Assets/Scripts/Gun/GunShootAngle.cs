using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class GunShootAngle : GunShootLimit
{
    [Tooltip("Must be odd.")]
    [ValidateInput(nameof(CheckAmountPerShotValue), "Amount per shoot must be odd.")]
    public int amountPerShot = 5;
    [Range(0f, 360f)]
    public float angleBetweenShots = 15f;

    public override void Shoot()
    {
        int mult = 0;
        for (int i = 0; i < amountPerShot; i++)
        {
            var projectile = Instantiate(prefabProjectile, shootPos.position, shootPos.rotation, shootPos);
            projectile.transform.localEulerAngles = Vector3.zero + Vector3.up * (i%2 == 0?angleBetweenShots : -angleBetweenShots) * mult;
            projectile.speed = projetileSpeed;
            projectile.transform.parent = null;

            if(i%2 == 0)
            {
                mult++;
            }
        }
    }

    private bool CheckAmountPerShotValue(int amount)
    {
        return amount%2 != 0;
    }

}
