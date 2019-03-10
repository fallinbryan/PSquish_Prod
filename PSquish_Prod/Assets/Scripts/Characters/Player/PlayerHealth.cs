using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private const int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    
    void Awake()
    {
        Debug.Log("Health Slider loaded");
        currentHealth = startingHealth;
        healthSlider.value = startingHealth;
    }
    
    void Update()
    {
       
    }
    
    public void TakeDamage(int amount)
    {
        Debug.Log("Taking damage: " + amount);
        currentHealth -= amount;
        healthSlider.value = currentHealth;
    }


    public void Add(int amount)
    {
        Debug.Log("Adding Health : " + amount);
        currentHealth += amount;
        healthSlider.value = currentHealth;
    }

    public void Reset()
    {
        Debug.Log("Reseting Health : ");
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

}