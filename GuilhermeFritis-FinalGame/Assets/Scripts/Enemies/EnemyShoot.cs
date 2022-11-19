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
            base.Init();

            gunBase.SetActiveWeapon(true);
        }

        protected override void Die()
        {
            gunBase.StopShoot();
            gunBase.SetActiveWeapon(false);
            base.Die();
        }

        public override void WakeUp(GameObject player)
        {
            base.WakeUp(player);
            
            if(gunBase != null)
            {
                gunBase.StartShoot();
            }
        }

        public override void Sleep()
        {
            base.Sleep();

            if(gunBase != null)
            {
                gunBase.StopShoot();
            }
        }

        private void OnDestroy() 
        {
            gunBase.StopShoot();
            gunBase.SetActiveWeapon(false);
        }
    }
}
