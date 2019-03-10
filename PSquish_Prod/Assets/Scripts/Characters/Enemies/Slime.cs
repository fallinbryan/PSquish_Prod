using ProfessorSquish.Components.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProfessorSquish.Characters.Enemies
{
    public class Slime : MonoBehaviour
    {
        public GameObject Player;
        public int maxPursueDistance = 30;

        private readonly Wander wander = new Wander();
        private int minPursueDistance = 1; //To stop enemy from dry humping player
        private NavMeshAgent agent;
        private float wanderRadius = 40f;
        private int currentHP;
        private int maxHP = 10;
        private float wanderTimer = 5f;
        private float timer = 0f;
        private readonly float MoveSpeed = 3.5f;
        private float attackTime; 
        private float attackSpeed = 5f;
        private Animator ani;

        [SerializeField]
        public GameObject Muzzle;
        [SerializeField]
        public bool isRanged;
        [SerializeField]
        public GameObject Projectile;
        [SerializeField]
        public GameObject HealthDropPrefab;
        [SerializeField]
        public GameObject AmmoDropPrefab;

        private ScoreManager scoreManager;

        // Start is called before the first frame update

        void Awake()
        {
            scoreManager = GetComponent<ScoreManager>();
        }

        void Start()
        {
            
            Player = GameObject.FindWithTag("Player");
            agent = GetComponent<NavMeshAgent>();

            //So they spawn with full health each time
            currentHP = maxHP;

            ani = GetComponent<Animator>();

        }
        void OnGUI()
        {
            if (Vector3.Distance(this.transform.position, Player.transform.position) <= maxPursueDistance)
            {
                Vector2 targetPosition = Camera.main.WorldToScreenPoint(this.transform.position);
                GUI.Box(new Rect(targetPosition.x - 35, Screen.height - targetPosition.y - 50, 60, 20), currentHP + "/" + maxHP);

            }
        }

        // Update is called once per frame
        void Update()
        {
            //Called as first thing, so if it is dead we don't waste cpu
            ani.SetFloat("moveSpeed", agent.velocity.magnitude);
            if (currentHP <= 0)
            {
                scoreManager.Increase(1);

                var pc = Player.GetComponent<Player.PlayerController>();

                int health = 0;
                int ammo = 0;

                //This is to prevent any null references
                if (pc != null)
                {
                    health = pc.playerHealth;
                    ammo = pc.WeaponController.WeaponAmmo;
                }
                if (HealthDropPrefab == null)
                {
                    HealthDropPrefab = GameObject.FindWithTag("Health");
                }
                if (AmmoDropPrefab == null)
                {
                    AmmoDropPrefab = GameObject.FindWithTag("Ammo");
                }
                


                //Drop health
                if (health < ammo)
                {
                    //var healthObj = GameObject.FindWithTag("Health");
                    var h = Instantiate(HealthDropPrefab, agent.transform.position, agent.transform.rotation) as GameObject;
                    h.transform.position = agent.transform.position;
                }
                //drop ammo
                else if (health > ammo)
                {
                    //var ammoObj = GameObject.FindWithTag("Ammo");
                    var h = Instantiate(AmmoDropPrefab, agent.transform.position, agent.transform.rotation) as GameObject;
                    h.transform.position = agent.transform.position;
                }
                //drop health
                else
                {
                    //var healthObj = GameObject.FindWithTag("Health");
                    var h = Instantiate(HealthDropPrefab, agent.transform.position, agent.transform.rotation) as GameObject;
                    h.transform.position = agent.transform.position;
                }
                pc.WeaponController.WeaponExp += 100;
                Destroy(this.gameObject);
                return;
            }

            //transform.rotation = Quaternion.Euler(-90.0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            attackTime += Time.deltaTime;
            if (Vector3.Distance(transform.position, Player.transform.position) <= maxPursueDistance)
            {
                transform.LookAt(Player.transform);
                //  transform.LookAt(new Vector3(Player.transform.position.x, 1.0f, Player.transform.position.z));

                if (isRanged && attackTime >attackSpeed)
                {
                    var bullet = Instantiate(Projectile, Muzzle.transform.position, Muzzle.transform.rotation) as GameObject;
                    bullet.GetComponent<Rigidbody>().velocity = bullet.transform.TransformDirection(Vector3.forward * 100);
                    attackTime = 0;
                }
                //This is so the enemy doesn't dry hump the player
                if (Vector3.Distance(transform.position, Player.transform.position) > minPursueDistance)
                {
                    Vector3 m = new Vector3(1f, 1f, 1f);
                    m = transform.forward * MoveSpeed * Time.deltaTime;

                    transform.position += m;
                    ani.SetBool("isAttacking", true);
                }
                else if (!isRanged)
                {
                    ani.SetBool("isAttack", true);
                }
            }
            else
            {
                timer += Time.deltaTime;
                ani.SetBool("isAttacking", false);
                if (timer >= wanderTimer)
                {
                    var newLocation = wander.WanderLocation(transform.position, wanderRadius);
                    agent.SetDestination(newLocation);
                    transform.LookAt(newLocation);

                    timer = 0;
                }

            }
        }
        void OnParticleCollision(GameObject other)
        {
            switch (other.gameObject.tag)
            {
                case "Bullet":
                    currentHP--;
                    break;
                default:
                    break;
            }
        }
        void OnCollisionEnter(Collision collision)
        {
            switch (collision.gameObject.tag)
            {
                case "Bullet":
                    currentHP--;
                    break;
                default:
                    break;
            }
        }
    }

}
