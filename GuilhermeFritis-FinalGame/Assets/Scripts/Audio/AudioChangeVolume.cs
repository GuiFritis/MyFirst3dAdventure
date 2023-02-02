using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Sounds
{
    public class AudioChangeVolume : MonoBehaviour
    {
        public AudioMixer group;
        public string floatParam = "MyExposedParam";

        public void ChangeVolume(float f){
            group.SetFloat(floatParam, f);
        }
    }
}