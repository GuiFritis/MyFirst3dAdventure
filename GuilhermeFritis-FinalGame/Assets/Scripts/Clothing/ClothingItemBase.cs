using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clothing {
    public class ClothingItemBase : MonoBehaviour
    {
        public ClothingType clothType;
        public string playerTag = "Player";
        public float duration = 3f;

        void OnTriggerEnter(Collider collision)
        {
            if(collision.transform.CompareTag(playerTag)){
                Collect();
            }
        }

        public virtual void Collect()
        {
            var setup = ClothingManager.Instance.GetSetupByType(clothType);

            Player.Instance.ChangeTexture(setup, duration);

            HideObject();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}