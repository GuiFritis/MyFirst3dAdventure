using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clothing 
{
    public class ClothingItemSpeed : ClothingItemBase
    {
        [Header("Speed Cloth")]
        public float speedMultiplier = 1.2f;

        public override void Collect()
        {
            base.Collect();

            Player.Instance.ChangeSpeed(speedMultiplier, duration);
        }
    }
}
