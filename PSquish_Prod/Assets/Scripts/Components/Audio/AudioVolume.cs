using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProfessorSquish.Components.Audio
{
    public class AudioVolume : MonoBehaviour
    {
        public Slider Volume;
        public AudioSource myAudio;
        public AudioSource myAudio1;
        public AudioSource myAudio2;
        public AudioSource myAudio3;
        public AudioSource myAudio4;
        public AudioSource myAudio5;
        public AudioSource myAudio6;
        public AudioSource myAudio7;
        public AudioSource myAudio8;
        public AudioSource myAudio9;
        public AudioSource myAudio10;
        public AudioSource myAudio11;
        public AudioSource myAudio12;
        public AudioSource myAudio13;

        public void Start()
        {
            myAudio.volume = Volume.value;
            myAudio1.volume = Volume.value;
            myAudio2.volume = Volume.value;
            myAudio3.volume = Volume.value;
            myAudio4.volume = Volume.value;
            myAudio5.volume = Volume.value;
            myAudio6.volume = Volume.value;
            myAudio7.volume = Volume.value;
            myAudio8.volume = Volume.value;
            myAudio9.volume = Volume.value;
            myAudio10.volume = Volume.value;
            myAudio11.volume = Volume.value;
            myAudio12.volume = Volume.value;
            myAudio13.volume = Volume.value;
        }

        public void AdjustVolume()
        {
            myAudio.volume = Volume.value;
            myAudio1.volume = Volume.value;
            myAudio2.volume = Volume.value;
            myAudio3.volume = Volume.value;
            myAudio4.volume = Volume.value;
            myAudio5.volume = Volume.value;
            myAudio6.volume = Volume.value;
            myAudio7.volume = Volume.value;
            myAudio8.volume = Volume.value;
            myAudio9.volume = Volume.value;
            myAudio10.volume = Volume.value;
            myAudio11.volume = Volume.value;
            myAudio12.volume = Volume.value;
            myAudio13.volume = Volume.value;
        }

 

    }
}