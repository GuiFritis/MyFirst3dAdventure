using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sounds
{
    public class AudioRandomPlayClip : MonoBehaviour
    {
        public List<AudioClip> audiosClips;

        public void PlayRandomAudio(){
            SFX_Pool.Instance.Play(audiosClips[Random.Range(0, audiosClips.Count)]);
        }

    }
}
