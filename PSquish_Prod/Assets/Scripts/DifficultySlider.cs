using UnityEngine;
using UnityEngine.UI;
using ProfessorSquish.Components.Audio;


public class DifficultySlider : MonoBehaviour
{
    private const int defaultDiff = 2;
    public float currentDiff;
    public Slider diffSlider;
    AudioSource audioData;

    void Awake()
    {
        Debug.Log("Difficulty Slider loaded");
        currentDiff = defaultDiff;
        diffSlider.value = currentDiff;
    }

    void Start()
    {
        diffSlider.onValueChanged.AddListener(delegate { UpdateValue(); });
        audioData = GetComponent<AudioSource>();
    }

 
    void UpdateValue()
    {
        Debug.LogFormat("Updating value of diffSlider to {0}", diffSlider.value);

        if (diffSlider.value == 1)
        {
            SoundManagerScript.PlayOneShot("Kid_Laugh");
        }
        else if(diffSlider.value == 2)
        {
            SoundManagerScript.PlayOneShot("Laughter");
        }
        else if (diffSlider.value == 3)
        {
            SoundManagerScript.PlayOneShot("Maniac");
        }


    }


}