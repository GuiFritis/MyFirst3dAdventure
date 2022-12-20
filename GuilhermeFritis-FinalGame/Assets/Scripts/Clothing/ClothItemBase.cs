using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clothing {
    public class ClothItemBase : MonoBehaviour
    {
        public ClothType clothType;
        public string playerTag = "Player";

        void OnTriggerEnter(Collider collision)
        {
            if(collision.transform.CompareTag(playerTag)){
                Collect();
            }
        }

        public virtual void Collect()
        {
            HideObject();
        }

        private void HideObject()
        {
            gameObject.SetActive(false);
        }
    }
}