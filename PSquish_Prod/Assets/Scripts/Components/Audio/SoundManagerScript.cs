using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProfessorSquish.Components.Audio
{

    public class SoundManagerScript : MonoBehaviour
    {
        static AudioSource audioSrc;
       

        HashSet<string> sounds = new HashSet<string>();
        private static Dictionary<string, AudioSource> soundResources = new Dictionary<string, AudioSource>();
        private static Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
        public static float Volume;

        public static void setVolume(float vol)
        {
            if (vol > 0.0f && vol < 1.0f)
            {
                Volume = vol;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            //https://freesound.org/people/cabled_mess/sounds/350899/

            sounds.Add("jump");
            sounds.Add("flames");
            sounds.Add("acidSpray");
            sounds.Add("lightning");
            sounds.Add("standardFire");
            sounds.Add("damage1");

            //http://soundbible.com/2026-Kid-Laugh.html
            sounds.Add("Kid_Laugh");

            sounds.Add("Laughter");

            //http://soundbible.com/2096-Maniacal-Laugh-2.html
            sounds.Add("Maniac");

            sounds.Add("Story");

            sounds.Add("coin");


            foreach (string e in sounds)
            {
                AudioClip clip = Resources.Load<AudioClip>(e);
                AudioSource source = gameObject.AddComponent<AudioSource>();
                source.clip = clip;
                soundResources.Add(e, source);
                audioClips.Add(e, clip);
            }

            audioSrc = gameObject.AddComponent<AudioSource>();

        }

        // Update is called once per frame
        void Update()
        {

        }

        public static void PlayOneShot(string clip)
        {
            
            soundResources[clip].Stop();
            Debug.LogFormat("Playing clip {0}", clip);
            audioSrc.PlayOneShot(audioClips[clip],Volume);

            

        }

        public static void PlayLongSound(string clip)
        {
            //Slider Volume = GameObject.Find("VolumeSlider").GetComponent<Slider>();
            if (soundResources[clip].isPlaying) return;
            Debug.LogFormat("Playing Independent clip {0}", clip);
            soundResources[clip].volume = Volume;
            soundResources[clip].Play();
            //audioSrc.PlayOneShot(audioClips[clip], Volume.value);

        }


        public static void StopSound(string clip)
        {
            Debug.LogFormat("Stopping clip {0}", clip);
            soundResources[clip].Stop();

        }


        public static AudioSource GetSoundResource(string clip)
        {
            return soundResources[clip];

        }
    }
}
