using System;
using UnityEngine;

namespace Sounds
{
    public class SFX_Pool : PoolBase<AudioSource>
    {
        public static SFX_Pool Instance = null;

        protected override void Singleton()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else 
            {
                Destroy(this);
            }
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public void Play(sfxType sfxType)
        {
            if(sfxType != sfxType.NONE)
            {
                var sfx = SoundManager.Instance.GetSfxByType(sfxType);
                if(sfx != null)
                {
                    Play(sfx.audioClip);
                }
            }
        }

        public void Play(AudioClip clip)
        {
            if(clip != null)
            {
                var item = GetPoolItem();
                item.clip = clip;
                item.Play();
            }
        }

        protected override bool CheckItem(AudioSource item)
        {
            return !item.isPlaying;
        }
    }
}