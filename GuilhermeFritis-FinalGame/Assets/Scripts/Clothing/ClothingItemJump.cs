using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clothing 
{
    public class ClothingItemJump : ClothingItemBase
    {
        [Header("Jump Cloth")]
        public float jumpForceMultiplier = 1.4f;

        public override void Collect()
        {
            base.Collect();

            Player.Instance.ChangeJumpForce(jumpForceMultiplier, duration);
        }
    }
}

