using UnityEngine;
using UnityEngine.UI;


namespace ProfessorSquish.Characters.Player
{


    public class PlayerHealth : MonoBehaviour
    {
        private const int startingHealth = 20;
        public int currentHealth;
        public Slider healthSlider;                         
        bool damaged;
        public Image damageImage;                                  
        public float flashSpeed = 5f;                              
        public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     

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
           
            if (damaged)
            {
               Debug.Log("Changing damageImage color");
               damageImage.color = flashColour;
            }
           
            else
            {
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }

          
            damaged = false;

			if(currentHealth > startingHealth)
			{
				currentHealth = startingHealth;
			}
        }

        public void TakeDamage(int amount)
        {
            damaged = true;
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