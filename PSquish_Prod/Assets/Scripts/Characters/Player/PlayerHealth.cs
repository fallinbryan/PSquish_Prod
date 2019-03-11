using UnityEngine;
using UnityEngine.UI;


namespace ProfessorSquish.Characters.Player
{


    public class PlayerHealth : MonoBehaviour
    {
        private const int startingHealth = 100;
        public int currentHealth;
        public Slider healthSlider;

        public PlayerHealth(int hp)
        {
            currentHealth = hp;
        }

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
}