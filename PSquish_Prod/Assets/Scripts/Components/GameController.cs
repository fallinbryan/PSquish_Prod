using ProfessorSquish.Components.Audio;
using ProfessorSquish.Characters.Player;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace ProfessorSquish.Components
{

    public class GameController : MonoBehaviour
    {
        public bool isPaused;
        private bool isInventoryShown;

        public CameraController cameraController;
        public AudioVolume volumeControl;

        [SerializeField]
        public PlayerController player;

        [SerializeField]
        public GameObject CurrentWeaponPanel;

        [SerializeField]
        public GameObject MainMenu;

        [SerializeField]
        public GameObject InventoryPanel;

        [SerializeField]
        public GameObject helpPanel;

        [SerializeField]
        public GameObject helpPanelPage2;

        [SerializeField]
        public GameObject storyAudio;

        [SerializeField]
        public GameObject upgradePanel;

        [SerializeField]
        public Utilities.DialogController dialogController;

        private Transform currentWeponImageLoc;

        [SerializeField]
        public GameObject inventoryButton;

        public GameObject resumeButton;

        [SerializeField]
        public GameObject difficultySlider;

        private GameObject currentHelpPanel;

        void Awake()
        {
            inventoryButton = GameObject.Find("InventoryButton");
            resumeButton = GameObject.Find("ResumeButton");
            difficultySlider = GameObject.Find("DifficultySlider");
            //helpPanel = GameObject.Find("HelpPanel");
            storyAudio = GameObject.Find("Mute");
            player = GameObject.Find("Player").GetComponent<PlayerController>();
            currentHelpPanel = helpPanel;
        }


        // Start is called before the first frame update
        void Start()
        {

            ShowStartMenu();
            HideHelpMenu();

            currentWeponImageLoc = CurrentWeaponPanel.transform;

            foreach (Image image in CurrentWeaponPanel.GetComponentsInChildren<Image>())
            {
                if (image.gameObject.tag != "WeaponBase" && image.gameObject.tag != "Untagged")
                {
                    image.transform.position = currentWeponImageLoc.transform.position;
                    image.enabled = false;

                }

            }
        }

        private void ShowStartMenu()
        {
            resumeButton.GetComponentInChildren<Text>().text = "Play!";
            ShowMainMenu();
            inventoryButton.SetActive(false);
            pauseGame();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    continueGame();
                }
                else
                {
                    pauseGame();
                }

            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                foreach (Image image in CurrentWeaponPanel.GetComponentsInChildren<Image>())
                {

                    if (image.gameObject.tag != "WeaponBase" && image.gameObject.tag != "Untagged")
                    {
                        image.enabled = false;
                    }
                    else if (image.gameObject.tag == "WeaponBase")
                    {
                        image.enabled = true;
                        image.transform.position = currentWeponImageLoc.position;
                    }
                }

            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && player.WeaponController.isModeEnabled("Flames"))
            {

                foreach (Image image in CurrentWeaponPanel.GetComponentsInChildren<Image>())
                {
                    if (image.gameObject.tag != "WeaponFlame" && image.gameObject.tag != "Untagged")
                    {
                        image.enabled = false;
                    }
                    else if (image.gameObject.tag == "WeaponFlame")
                    {
                        image.enabled = true;
                        image.transform.position = currentWeponImageLoc.position;
                    }
                }



            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && player.WeaponController.isModeEnabled("AcidSpray"))
            {

                foreach (Image image in CurrentWeaponPanel.GetComponentsInChildren<Image>())
                {
                    if (image.gameObject.tag != "WeaponAcid" && image.gameObject.tag != "Untagged")
                    {
                        image.enabled = false;
                    }
                    else if (image.gameObject.tag == "WeaponAcid")
                    {
                        image.enabled = true;
                        image.transform.position = currentWeponImageLoc.position;
                    }
                }


            }
            if (Input.GetKeyDown(KeyCode.Alpha4) && player.WeaponController.isModeEnabled("Lightning"))
            {

                foreach (Image image in CurrentWeaponPanel.GetComponentsInChildren<Image>())
                {
                    if (image.gameObject.tag != "WeaponTesla" && image.gameObject.tag != "Untagged")
                    {
                        image.enabled = false;
                    }
                    else if (image.gameObject.tag == "WeaponTesla")
                    {
                        image.enabled = true;
                        image.transform.position = currentWeponImageLoc.position;
                    }
                }

            }

        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            Debug.Log("Game is running in the editor , pausing it");
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Debug.Log("Closing the game");
             Application.Quit();
#endif
        }

        public void continueGame()
        {
            resumeButton.GetComponentInChildren<Text>().text = "Resume";
            difficultySlider.SetActive(false);
            inventoryButton.SetActive(true);
            isPaused = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;

            cameraController.isGamePaused = false;
            player.isPaused = false;

            HideMainMenu();
            HideInventoryMenu();
        }



        private void pauseGame()
        {
            isPaused = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;

            cameraController.isGamePaused = true;
            player.isPaused = true;

            ShowMainMenu();
        }

        public void AdjustVolume()
        {
            volumeControl.AdjustVolume();
        }

        public void ShowMainMenu()
        {
            MainMenu.gameObject.SetActive(true);
        }

        public void ShowInventoryMenu()
        {
            InventoryPanel.GetComponent<InventoryPanelController>().show();
        }

        public void ShowHelpMenu()
        {
            //MutePlayHelpMenuStory();
            //currentHelpPanel = Instantiate(helpPanel, GameObject.Find("Canvas").gameObject.transform);
            currentHelpPanel.SetActive(true);
        }

        public void HideHelpMenu()
        {
            SoundManagerScript.StopSound("Story");
            //helpPanel.SetActive(false);
            currentHelpPanel = helpPanel;
            helpPanel.SetActive(false);
            helpPanelPage2.SetActive(false);
        }

        public void MutePlayHelpMenuStory()
        {
            Debug.LogFormat(" MutePlayHelpMenuStory {0}", SoundManagerScript.GetSoundResource("Story").isPlaying);

            if (SoundManagerScript.GetSoundResource("Story").isPlaying)
            {
                SoundManagerScript.StopSound("Story");
            }
            else
            {
                SoundManagerScript.PlayLongSound("Story");
            }

        }


        public void HideMainMenu()
        {
            MainMenu.gameObject.SetActive(false);
            if(currentHelpPanel)
            {
            HideHelpMenu();

            }
        }

        public void HideInventoryMenu()
        {
            InventoryPanel.GetComponent<InventoryPanelController>().hide();
        }

        public void toggleInventoryMenu()
        {
            if (isInventoryShown)
            {
                isInventoryShown = false;
                HideInventoryMenu();
            }
            else
            {
                isInventoryShown = true;
                ShowInventoryMenu();
            }
        }

        public void attemptUpgrade()
        {
            Dictionary<string, List<string>> result = player.WeaponController.UpgradeWeapon(upgradePanel);
            foreach (KeyValuePair<string, List<string>> kvp in result)
            {
                if (kvp.Key != "Fail")
                {
                    dialogController.Dialog($"Upgraded weapon to {kvp.Key} mode");
                    foreach (string item in kvp.Value)
                    {
                        foreach (Image image in upgradePanel.GetComponentsInChildren<Image>())
                        {
                            if (image.gameObject.tag == item)
                            {
                                Destroy(image.gameObject);
                            }
                        }
                    }
                    break;
                }
                else
                {
                    if (kvp.Value != null)
                    {
                        dialogController.Dialog(kvp.Value[0]);

                    }
                }
            }
        }

        public void HelpPanelNextButtonClick() {
            switch (currentHelpPanel.gameObject.tag) {
                case "PrimaryHelpPanel":

                    currentHelpPanel.SetActive(false);
                    currentHelpPanel = helpPanelPage2;
                    currentHelpPanel.SetActive(true);

                    break;
                case "WeponUpgradeHelpPanel":
                    //do nothing
                    break;
                default:
                    break;
            }

        }

        public void HelpPanelPrevButtonClick() {
            switch (currentHelpPanel.gameObject.tag)
            {
                case "PrimaryHelpPanel":
                    //do nothing
                    break;
                case "WeponUpgradeHelpPanel":
                    currentHelpPanel.SetActive(false);
                    currentHelpPanel = helpPanel;
                    currentHelpPanel.SetActive(true);
                    break;
                default:
                    break;
            }
        }


    }
}
