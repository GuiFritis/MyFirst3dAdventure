using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sounds{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        public MusicType musicType;
        public AudioSource audioSource;

        private MusicSetup _currentMusicSetup;

        void OnValidate()
        {
            audioSource = GetComponent<AudioSource>();
        }

        void Start()
        {
            Play();
        }

        private void Play() {
            _currentMusicSetup = SoundManager.Instance.GetMusicByType(musicType);

            audioSource.clip = _currentMusicSetup.audioClip;
            audioSource.Play();
        }
    }
}
