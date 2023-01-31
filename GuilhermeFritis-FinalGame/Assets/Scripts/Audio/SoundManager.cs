using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;

namespace Sounds
{
    public class SoundManager : Singleton<SoundManager>
    {
        public List<MusicSetup> musicSetups = new List<MusicSetup>();
        public List<sfxSetup> sfxSetups = new List<sfxSetup>();
        public AudioSource musicSource;

        public void PlayMusicByType(MusicType type)
        {
            var setup = GetMusicByType(type);
            musicSource.clip = setup.audioClip;
            musicSource.Play();
        }

        public MusicSetup GetMusicByType(MusicType type)
        {
            return musicSetups.Find(i => i.musicType == type);
        }

        public sfxSetup GetSfxByType(sfxType type)
        {
            return sfxSetups.Find(i => i.sfxType == type);
        }
    }

    public enum MusicType
    {
        TYPE_01,
        TYPE_02,
        TYPE_03
    }

    [System.Serializable]
    public class MusicSetup
    {
        public MusicType musicType;
        public AudioClip audioClip;
    }

    public enum sfxType
    {
        TYPE_01,
        TYPE_02,
        TYPE_03
    }

    [System.Serializable]
    public class sfxSetup
    {
        public sfxType sfxType;
        public AudioClip audioClip;
    }

}