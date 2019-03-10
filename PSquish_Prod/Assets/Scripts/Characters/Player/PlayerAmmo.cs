using UnityEngine;
using UnityEngine.UI;

public class PlayerAmmo : MonoBehaviour
{
    public Slider ammoSlider;
      
    void Awake()
    {
        Debug.Log("Ammo Slider loaded");
    }

    void Update()
    {

    }

    public float Get()
    {
        return ammoSlider.value;
    }


    public void Set(int v)
    {
        ammoSlider.value = v;
        Debug.LogFormat("Setting Ammo Slider value {0}", v);
    }

    public void Add(int v)
    {
        ammoSlider.value += v;
        Debug.LogFormat("Increasing Ammo Slider by {0}", v);
    }

    public Slider Take(int v)
    {
        ammoSlider.value -= v;
        Debug.LogFormat("Taking Ammo value {0}", v);
        return ammoSlider;
    }


}