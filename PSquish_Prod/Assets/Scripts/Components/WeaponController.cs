using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProfessorSquish.Components.Audio;


namespace ProfessorSquish.Components
{
    public class WeaponController : MonoBehaviour
    {
        [SerializeField]
        public GameObject Muzzle;

        [SerializeField]
        public GameObject TeslaCoil;

        [SerializeField]
        public GameObject Igniter;

        [SerializeField]
        public GameObject FlameFuelTank;

        [SerializeField]
        public GameObject BatteryPack;

        [SerializeField]
        public GameObject AcidFuelTank;

        [SerializeField]
        public ParticleSystem Flames;

        [SerializeField]
        public ParticleSystem AcidSpray;

        [SerializeField]
        public ParticleSystem Lightning;

        [SerializeField]
        private GameObject Projectile;

        [SerializeField]
        public Slider expSlider;


        public int projectileVelocity;

        [Range(0, 1000)]
        public int WeaponAmmo = 100;

        public int WeaponExp = 0;

        [Range(0f, 1f)]
        public float attackDelta = 0.05f;
        private float attackTime = 0;

        private Dictionary<string, GameObject> WeaponParts;
        private Dictionary<string, ParticleSystem> WeaponEmission;
        private Dictionary<string, bool> enabledModes;
        private Dictionary<string, int> modeUpgradeThresholds;
        private Dictionary<string, string> sounds;
        private string ActiveWeaponMode;
        private bool weaponIsFiring = false;



        void Awake()
        {

        }


        // Start is called before the first frame update
        void Start()
        {
            TeslaCoil.SetActive(false);
            Igniter.SetActive(false);
            FlameFuelTank.SetActive(false);
            BatteryPack.SetActive(false);
            AcidFuelTank.SetActive(false);

            Flames.Stop();
            AcidSpray.Stop();
            Lightning.Stop();


            WeaponParts = new Dictionary<string, GameObject>
            {
                { "TeslaCoil", TeslaCoil },
                { "Igniter", Igniter },
                { "FlameFuelTank", FlameFuelTank },
                { "BatteryPack", BatteryPack },
                { "AcidFuelTank", AcidFuelTank }
            };

            WeaponEmission = new Dictionary<string, ParticleSystem>
            {
                { "Flames", Flames },
                { "AcidSpray", AcidSpray },
                { "Lightning", Lightning }
            };

            ActiveWeaponMode = "standard";

            enabledModes = new Dictionary<string, bool>
            {
                { "standard", true },
                { "Flames", false },
                { "AcidSpray", false },
                { "Lightning", false }
            };

            modeUpgradeThresholds = new Dictionary<string, int>
            {
                { "AcidSpray", 300 },
                { "Flames", 1000 },
                { "Lightning", 5000 }
            };

            sounds = new Dictionary<string, string>
            {
                { "standard", "standardFire" },
                { "Flames", "flames" },
                { "AcidSpray", "acidSpray" },
                { "Lightning", "lightning" }
            };

        }

        // Update is called once per frame
        void Update()
        {
            attackTime += Time.deltaTime;

            if (expSlider)
            {
                expSlider.value = WeaponExp;
                if (weaponIsFiring)
                {
                    WeaponAmmo -= 1;
                    FireWeapon();

                }

            }
            else
            {
                //Debug.Log("Where the fuck did the experience slider go??");
            }

        }

        public bool SetActiveWeaponMode(string mode)
        {
            // if(enabledModes[mode])
            // {
            ActiveWeaponMode = mode;

            // }
            return enabledModes[mode];
        }

        public int FireWeapon()
        {

            if (!SoundManagerScript.GetSoundResource(sounds[ActiveWeaponMode]).isPlaying)
            {
                SoundManagerScript.PlayLongSound(sounds[ActiveWeaponMode]);
            }

            if (ActiveWeaponMode == "standard")
            {
                if (attackTime > attackDelta)
                {
                    GameObject WeaponEmission;
                    WeaponEmission = Instantiate(Projectile, Muzzle.transform.position, Muzzle.transform.rotation) as GameObject;
                    WeaponEmission.GetComponent<Rigidbody>().velocity = WeaponEmission.transform.TransformDirection(Vector3.forward * projectileVelocity);
                    attackTime = 0;
                    WeaponAmmo -= 10;
                }
            }

            else if (!WeaponEmission[ActiveWeaponMode].isPlaying)
            {
                weaponIsFiring = true;
                WeaponEmission[ActiveWeaponMode].Play(true);
            }

            return WeaponAmmo;
        }
        public void StopFiringWeapon()
        {
            if (ActiveWeaponMode == "standard")
            {
                Flames.Stop();
                AcidSpray.Stop();
                Lightning.Stop();
                return;
            }
            else
            {
                //WeaponEmission[ActiveWeaponMode].Stop(true, ParticleSystemStopBehavior.StopEmitting);
                weaponIsFiring = false;
                WeaponEmission[ActiveWeaponMode].Stop();
            }

        }

        public Dictionary<string, List<string>> UpgradeWeapon(GameObject upgradePanel)
        {

            Dictionary<string, List<string>> returnVal = new Dictionary<string, List<string>>();
            returnVal["Fail"] = null;
            Image[] images = upgradePanel.GetComponentsInChildren<Image>();
            List<string> parts = new List<string>();
            foreach (Image part in images)
            {
                if (part.gameObject.tag != "Untagged")
                {
                    parts.Add(part.gameObject.tag);
                }
            }
            if (parts.Count == 2)
            {
                if (parts.Contains("FlameFuelTank") && parts.Contains("Igniter"))
                {
                    if (WeaponExp < modeUpgradeThresholds["Flames"])
                    {
                        returnVal["Fail"] = new List<string> { "Not enough Experience to Upgrade, kill more slimes to gain exp" };
                    }
                    else if (!enabledModes["Flames"])
                    {
                        enabledModes["Flames"] = true;
                        WeaponParts["Igniter"].SetActive(true);
                        WeaponParts["FlameFuelTank"].SetActive(true);
                        returnVal["Flame Thrower"] = new List<string> { "FlameFuelTank", "Igniter" };
                    }
                    else
                    {
                        returnVal["Fail"] = new List<string> { "Dev note. Part has already been consumed and should no longer be inventory" };
                    }

                }
                else if (parts.Contains("BatteryPack") && parts.Contains("TeslaCoil"))
                {
                    if (WeaponExp < modeUpgradeThresholds["Lightning"])
                    {
                        returnVal["Fail"] = new List<string> { "Not enough Experience to Upgrade, kill more slimes to gain exp" };
                    }
                    else if (!enabledModes["Lightning"])
                    {
                        enabledModes["Lightning"] = true;
                        WeaponParts["BatteryPack"].SetActive(true);
                        WeaponParts["TeslaCoil"].SetActive(true);
                        returnVal["Tesla Gun"] = new List<string> { "BatteryPack", "TeslaCoil" };
                    }
                    else
                    {
                        returnVal["Fail"] = new List<string> { "Dev note. Part has already been consumed and should no longer be inventory" };

                    }

                }

            }
            else if (parts.Count == 1 && parts.Contains("AcidFuelTank"))
            {
                if (WeaponExp < modeUpgradeThresholds["AcidSpray"])
                {
                    returnVal["Fail"] = new List<string> { "Not enough Experience to Upgrade, kill more slimes to gain exp" };
                }
                else if (!enabledModes["AcidSpray"])
                {
                    enabledModes["AcidSpray"] = true;
                    WeaponParts["AcidFuelTank"].SetActive(true);
                    returnVal["Acid Sprayer"] = new List<string> { "AcidFuelTank" };
                }
                else
                {
                    returnVal["Fail"] = new List<string> { "Dev note. Part has already been consumed and should no longer be inventory" };

                }

            }
            if (returnVal["Fail"] == null)
            {
                returnVal["Fail"] = new List<string> { "Incorrect Combination of parts" };

            }

            return returnVal;
        }
    }
}