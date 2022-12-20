using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clothing 
{
    public class ClothItemSpeed : ClothItemBase
    {
        [Header("Speed Cloth")]
        public float speedMultiplier = 1.2f;
        public float duration = 3f;

        public override void Collect()
        {
            base.Collect();
            Player.Instance.ChangeSpeed(speedMultiplier, duration);
        }
    }
}
