using ProfessorSquish.Components.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
        private float deathDelay = 2f;
        private float deathTimeCounter = 0f;
        private float attackAnimateTime = 0f;
        private bool attackReleased = false;

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
        [SerializeField]
        private float charge;
        [SerializeField]
        private ParticleSystem dischrg;
        private float time = 0f;
        private bool acid;
        [SerializeField]
        private ParticleSystem deathCloud;

        private float difficultyModifier = 2f;

        public void onDifficultyChange(Slider slider)
        {
            this.maxHP = (int)((slider.value / 2) * this.maxHP);
            this.currentHP = this.maxHP;
            this.attackSpeed *= (slider.value / 2);
        }
        // Start is called before the first frame update
        void Start()
        {
            Player = GameObject.FindWithTag("Player");
            agent = GetComponent<NavMeshAgent>();
            ani = GetComponent<Animator>();
            dischrg.Stop();

            maxHP = (int)(maxHP * difficultyModifier / 2);
            //So they spawn with full health each time
            currentHP = maxHP;

        }
        void OnGUI()
        {
            var gc = GameObject.FindWithTag("GameController").GetComponent<Components.GameController>();

            if (!gc.isPaused)
            {
                if (currentHP >= 0 && Vector3.Distance(this.transform.position, Player.transform.position) <= maxPursueDistance)
                {
                    Vector2 targetPosition = Camera.main.WorldToScreenPoint(this.transform.position);
                    var rect = new Rect(targetPosition.x - 35, Screen.height - targetPosition.y - 50, 60, 20);
                    GUI.Box(rect, currentHP + "/" + maxHP);

                }

            }

        }

        // Update is called once per frame
        void Update()
        {
            //Called as first thing, so if it is dead we don't waste cpu
            ani.SetFloat("moveSpeed", agent.velocity.magnitude);
            ani.SetInteger("hp", currentHP);

            if (currentHP <= 0)
            {
                deathTimeCounter += Time.deltaTime;
                if (acid)
                {
                    Instantiate(deathCloud, transform.position, Quaternion.identity);
                    acid = false;
                }
                if (deathTimeCounter > deathDelay)
                {
                    ScoreManager.Increase(1);

                    var pc = Player.GetComponent<Player.PlayerController>();

                    int health = 0;
                    int ammo = 0;


                    //This is to prevent any null references
                    if (pc != null)
                    {
                        health = pc.playerHealth.currentHealth;
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

                }

                return;
            }

            //transform.rotation = Quaternion.Euler(-90.0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            attackTime += Time.deltaTime;
            if (attackReleased)
            {
                attackAnimateTime += Time.deltaTime;
                if (attackAnimateTime > attackSpeed)
                {
                    attackAnimateTime = 0;
                    attackReleased = false;
                    ani.SetBool("isAttacking", false);
                    var bullet = Instantiate(Projectile, Muzzle.transform.position, Muzzle.transform.rotation) as GameObject;
                    bullet.GetComponent<Rigidbody>().velocity = bullet.transform.TransformDirection(Vector3.forward * 100);
                }
            }
            if (Vector3.Distance(transform.position, Player.transform.position) <= maxPursueDistance)
            {
                transform.LookAt(Player.transform);
                //  transform.LookAt(new Vector3(Player.transform.position.x, 1.0f, Player.transform.position.z));

                if (isRanged && attackTime > attackSpeed)
                {

                    attackTime = 0;
                    ani.SetBool("isAttacking", true);
                    attackReleased = true;

                }
                //This is so the enemy doesn't dry hump the player
                if (Vector3.Distance(transform.position, Player.transform.position) > minPursueDistance)
                {
                    Vector3 m = new Vector3(1f, 1f, 1f);
                    m = transform.forward * MoveSpeed * Time.deltaTime;

                    transform.position += m;
                    ani.SetBool("isAttacking", true);
                    attackReleased = true;
                }
                else if (!isRanged)
                {
                    ani.SetBool("isAttack", true);
                    attackReleased = true;
                }
            }
            else
            {
                timer += Time.deltaTime;
                //ani.SetBool("isAttacking", false);
                if (timer >= wanderTimer)
                {
                    var newLocation = wander.WanderLocation(transform.position, wanderRadius);
                    agent.SetDestination(newLocation);
                    transform.LookAt(newLocation);

                    timer = 0;
                }

            }


            if (time > 0f)
            {
                time -= Time.deltaTime;
            }
            else
            {
                dischrg.Stop();
            }
            if (charge > 0f)
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, charge);
                if (hitColliders.Length != 1)
                {
                    GameObject target = this.gameObject;
                    float close = charge * 10 * charge * 10;

                    for (int i = 1; i < hitColliders.Length; i++)
                    {
                        float distance = (transform.position - hitColliders[i].transform.position).sqrMagnitude;

                        if (distance < close)
                        {
                            close = distance;
                            switch (hitColliders[i].transform.tag)
                            {
                                case "Enemy":
                                    target = hitColliders[i].gameObject;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }

                    if (target != this.gameObject)
                    {
                        Discharge(target);
                    }
                }

                charge -= Time.deltaTime;
                //print("CHARGE:" + charge);
            }
            else
            {
                //print("No Charge");
                charge = 0f;
                acid = false;
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

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawSphere(transform.position, charge);
        }

        void Charge(float chargeMod)
        {
            charge = charge + chargeMod;
            //print(charge);
            acid = false;
        }

        void Discharge(GameObject target)
        {
            print("DISCHARGED");
            dischrg.transform.LookAt(target.transform);
            dischrg.Play(true);
            time = 0.5f;
            charge = 0f;
        }

        void Acid()
        {
            acid = true;
        }
    }

}
