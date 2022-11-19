using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class ItemCollectableBase : MonoBehaviour
    {
        public ItemType itemType;
        public string playerTag = "Player";
        public ParticleSystem collectParticleSystem;
        public float hideDelay = 1f;
        public Collider collider;
        
        [Header("Sounds")]
        public AudioSource audioSorce;

        void OnValidate()
        {
            if(collider == null)
            {
                collider = GetComponent<Collider>();
            }
        }
        
        void OnTriggerEnter(Collider collision)
        {
            if(collision.transform.CompareTag(playerTag)){
                Collect();
                if(collider != null){
                    collider.enabled = false;
                }
            }
        }

        protected virtual void Collect(){
            Invoke(nameof(HideObject), hideDelay);
            OnCollect();
            if(audioSorce != null){
                audioSorce.Play();
            }
        }

        protected void HideObject(){        
            gameObject.SetActive(false);
        }

        protected virtual void OnCollect(){
            ItemManager.Instance.AddByType(itemType);
            if(collectParticleSystem != null){
                collectParticleSystem.Play();
            }
        }
    }
}
