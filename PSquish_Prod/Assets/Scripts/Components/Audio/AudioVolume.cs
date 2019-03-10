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

        public void Start()
        {
            myAudio.volume = Volume.value;
        }

        public void AdjustVolume()
        {
            myAudio.volume = Volume.value;
        }

 

    }
}