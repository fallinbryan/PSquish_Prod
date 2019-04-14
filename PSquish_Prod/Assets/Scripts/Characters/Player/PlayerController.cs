using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProfessorSquish.Components;
using ProfessorSquish.Components.Audio;


namespace ProfessorSquish.Characters.Player
{
    public class PlayerController : MonoBehaviour
    {

        [SerializeField]
        public WeaponController WeaponController;

        [Range(0f, 10f)]
        public float moveSpeed = 5f;
		[Range(0f, 100f)]
		public float rotateSpeed = 25f;
        [Range(0.0f, 1.0f)]
        public float gravityScale = 0.07f;
        [Range(0f, 50f)]
        public float jumpForce = 7.6f;

        //[Range(0f,10f)]
        public float delayJumpTime = 0.5f;

        private float timecounter = 0f;
        private float attackTimeCounter = 0f;
        bool wasJumpPressed = false;
        bool jumpExecuted = false;
        private bool wasAttackButtonPressed = false;
        public float delayAttackTime = 0.5f;

        public PlayerHealth playerHealth;
       
        private CharacterController cont;

        private PlayerHealth playerHealthSlider;

        public bool isPaused = false;

        private Vector3 move;
        private Camera cam;
        private Animator animate;
        public Inventory inventory;
        public PlayerAmmo ammoSlider;

		private Vector3 respPosition;
        
        void Awake()
        {
            cont = GetComponent<CharacterController>();
            playerHealthSlider = GetComponent<PlayerHealth>();
            ammoSlider = GetComponent<PlayerAmmo>();
            cam = Camera.main;
        }

        // Start is called before the first frame update
        void Start()
        {

            animate = GetComponent<Animator>();
            //playerHealth = new PlayerHealth(200);
            inventory = new Inventory();
			respPosition = transform.position;
            

        }

        // Update is called once per frame
        void Update()
        {
            ammoSlider.Set(WeaponController.GetAmmo());

            Vector3 forward = cam.transform.forward;
            forward.y = 0;
            forward = forward.normalized;
            Vector3 right  = new Vector3(forward.z, 0, -forward.x);
            float horizontalAxis = Input.GetAxis("Horizontal");
            float verticalAxis = Input.GetAxis("Vertical");
            Quaternion rotation = Quaternion.AngleAxis(10 * Mathf.Abs(horizontalAxis) * moveSpeed * Time.deltaTime, Vector3.up);

            //move = (horizontalAxis * right) + (verticalAxis * forward);

            //animate.SetBool("isGrounded", cont.isGrounded);
            float ydir = move.y;
            move = (forward * verticalAxis) + (right * horizontalAxis);
            move = rotation * move;
            move = move.normalized * moveSpeed;
            move.y = ydir;
            
            if(wasJumpPressed)
            {
                timecounter += (Time.deltaTime);
                if(timecounter > delayJumpTime && !jumpExecuted)
                {
                    move.y = jumpForce;
                    jumpExecuted = true;
                }
            }

            if(jumpExecuted && cont.isGrounded && timecounter > delayJumpTime + .5f)
            {
                // player has jumped and landed
                wasJumpPressed = false;
                jumpExecuted = false;
                animate.SetBool("wasJumpPressed", false);
                timecounter = 0.0f;
            }

            if (Input.GetButtonDown("Jump") && cont.isGrounded)
            {
                if (!move.Equals(cont.isGrounded))
                {
                    // audio SFX 
                    SoundManagerScript.PlayOneShot("jump");
                }

               
              
                animate.SetBool("wasJumpPressed", true);
                wasJumpPressed = true;

               // move.y = jumpForce;

            }

  
            move.y += Physics.gravity.y * gravityScale;



            if (Mathf.Abs(verticalAxis) + Mathf.Abs(horizontalAxis) > 0.1f)
            {

                animate.SetBool("isRunning", true);
            }
            else
            {
                animate.SetBool("isRunning", false);
            }

            if(wasAttackButtonPressed)
            {
                attackTimeCounter += (Time.deltaTime);
                if(attackTimeCounter > delayAttackTime)
                {
                    FireWeapon();
           
                }
                
            }

			if(Input.GetKeyDown(KeyCode.Alpha1) && !wasAttackButtonPressed)
            {
                WeaponController.SetActiveWeaponMode("standard");
               
            }
			if (Input.GetKeyDown(KeyCode.Alpha2) && !wasAttackButtonPressed)
            {
                WeaponController.SetActiveWeaponMode("Flames");
              

            }
			if (Input.GetKeyDown(KeyCode.Alpha3) && !wasAttackButtonPressed)
            {
                WeaponController.SetActiveWeaponMode("AcidSpray");

            }
			if (Input.GetKeyDown(KeyCode.Alpha4) && !wasAttackButtonPressed)
            {
                WeaponController.SetActiveWeaponMode("Lightning");
            }

            if (Input.GetButtonDown("Fire1") && !isPaused && !wasAttackButtonPressed && WeaponController.GetAmmoAvaliable() > 0)
            {
                
                animate.SetBool("isAttacking", true);
                wasAttackButtonPressed = true;
               
            }

            if (WeaponController.GetAmmoAvaliable() <= 0)
            {
                stopFireWeapon();
            }
            
            if (Input.GetButtonUp("Fire1"))
            {
               
                stopFireWeapon();
                animate.SetBool("isAttacking", false);
                attackTimeCounter = 0;
                wasAttackButtonPressed = false;
            }

            cont.Move(move * Time.deltaTime);
            //transform.Rotate(rotation.eulerAngles);

			transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, new Vector3(move.x, 0f, move.z), moveSpeed * rotateSpeed * Time.deltaTime, 0f));

            animate.SetFloat("playerLife", playerHealth.currentHealth);

			if(transform.position.y <= -100)
			{
				respawn();
			}

        }

        private void FireWeapon()
        {

            int ammo = WeaponController.FireWeapon();


     
            
            RaycastHit hitInfo;
            if(Physics.SphereCast(transform.position, 2.0f, transform.forward, out hitInfo, 30.0f ))
            {
                if(hitInfo.collider.gameObject.tag == "Enemy")
                {
                    transform.LookAt(hitInfo.transform);

                }

            }
        

        }

        public void stopFireWeapon()
        {
            animate.SetBool("isAttacking", false);
            WeaponController.StopFiringWeapon();
        }
        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                case "EnemyBullet":
                    Debug.Log(" in EnemyBullet --------------------");
                    SoundManagerScript.PlayOneShot("damage1");
                    playerHealth.TakeDamage(1);
					Debug.Log(playerHealth.currentHealth);
                    break;
                case "Bullet":
                    Debug.Log(" in Bullet --------------------");
                    SoundManagerScript.PlayOneShot("damage1");
                    playerHealth.TakeDamage(1);
					Debug.Log(playerHealth.currentHealth);
                    break;
                default:
                    break;
            }
        }
        void OnTriggerEnter(Collider collided)
        {
            switch (collided.gameObject.tag)
            {
                case "EnemyBullet":
                    Debug.Log(" in EnemyBullet2 --------------------");
                    playerHealth.TakeDamage(1);
                    SoundManagerScript.PlayOneShot("damage1");
                    Debug.Log(playerHealth.currentHealth);
                    break;
                case "Bullet":
                    Debug.Log(" in Bullet2 --------------------");
                    playerHealth.TakeDamage(1);
                    SoundManagerScript.PlayOneShot("damage1");
                    Debug.Log(playerHealth.currentHealth);
                    break;
                case "Health":
                    playerHealth.Add(1);
                    playerHealthSlider.Add(1);
                    Destroy(collided.gameObject);
                    break;
                case "Ammo":

                    WeaponController.AddAmmo(20);
                    ammoSlider.Set(WeaponController.GetAmmo());
                    Destroy(collided.gameObject);
                    break;
                case "FlameFuelTank":
                    inventory.AddItem(collided.gameObject);
                    collided.gameObject.SetActive(false);
                    break;
                case "TeslaCoil":
                    inventory.AddItem(collided.gameObject);
                    collided.gameObject.SetActive(false);
                    break;
                case "AcidFuelTank":
                    inventory.AddItem(collided.gameObject);
                    collided.gameObject.SetActive(false);
                    break;
                case "Igniter":
                    inventory.AddItem(collided.gameObject);
                    collided.gameObject.SetActive(false);
                    break;
                case "BatteryPack":
                    inventory.AddItem(collided.gameObject);
                    collided.gameObject.SetActive(false);
                    break;

                default:
                    break;
            }
		}
			void OnParticleCollision(GameObject other)
			{
				switch (other.gameObject.tag)
				{
				case "EnemyBullet":
					Debug.Log(" in EnemyBullet2 --------------------");
					playerHealth.TakeDamage(1);
					SoundManagerScript.PlayOneShot("damage1");
					Debug.Log(playerHealth.currentHealth);
					break;
				case "Bullet":
					Debug.Log(" in Bullet2 --------------------");
					playerHealth.TakeDamage(1);
					SoundManagerScript.PlayOneShot("damage1");
					Debug.Log(playerHealth.currentHealth);
					break;
				default:
					break;
				}

          }

		public void respawn()
		{
			//print(Time.deltaTime);
			//yield return new WaitForSeconds(5);
			transform.position = respPosition;
			playerHealth.Reset(); 
			//return("RESPAWNED");
		}

    }

}
