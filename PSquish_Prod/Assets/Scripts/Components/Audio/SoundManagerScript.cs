using System.Collections.Generic;
using UnityEngine;

namespace ProfessorSquish.Components.Audio
{

    public class SoundManagerScript : MonoBehaviour
    {
        static AudioSource audioSrc;

        HashSet<string> sounds = new HashSet<string>();
        private static Dictionary<string, AudioSource> soundResources = new Dictionary<string, AudioSource>();
        private static Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

        // Start is called before the first frame update
        void Start()
        {
            //https://freesound.org/people/cabled_mess/sounds/350899/

            sounds.Add("jump");
            sounds.Add("flames");
            sounds.Add("acidSpray");
            sounds.Add("lightning");
            sounds.Add("standardFire");

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
            audioSrc.PlayOneShot(audioClips[clip]);

        }

        public static void PlayLongSound(string clip)
        {

            if (soundResources[clip].isPlaying) return;
            Debug.LogFormat("Playing Independent clip {0}", clip);
            soundResources[clip].Play();

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
