using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clothing 
{
    public class ClothingItemStrength : ClothingItemBase
    {
        [Header("Stregth Cloth")]
        public float resistance = 0.4f;

        public override void Collect()
        {
            base.Collect();

            Player.Instance.health.IncreaseResistance(resistance, duration);
        }
    }
}

