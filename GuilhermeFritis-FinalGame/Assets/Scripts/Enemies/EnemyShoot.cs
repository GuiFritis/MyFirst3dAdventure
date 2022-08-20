using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{    
    public class EnemyShoot : EnemyBase
    {
        public GunBase gunBase;

        protected override void Init()
        {
            Debug.Log("INIT");
            base.Init();

            gunBase.SetActiveWeapon(true);
            gunBase.StartShoot();
        }
    }
}
