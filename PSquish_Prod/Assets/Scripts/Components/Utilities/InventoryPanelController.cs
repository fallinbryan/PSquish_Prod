using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProfessorSquish.Characters.Player;

public class InventoryPanelController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject inventoryPanel;
    public PlayerController player;
    public GameObject AcidFuelTankSlot;
    public GameObject FlameFuelTankSlot;
    public GameObject IgniterSlot;
    public GameObject BatteryPackSlot;
    public GameObject TeslaCoilSlot;

    private Dictionary<string,GameObject> slots;
    void Start()
    {
        inventoryPanel = GameObject.Find("InventoryPanel");
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        inventoryPanel.gameObject.SetActive(false);
 

        slots = new Dictionary<string, GameObject>
        {
            { "FlameFuelTank", FlameFuelTankSlot },
            { "TeslaCoil", TeslaCoilSlot },
            { "AcidFuelTank", AcidFuelTankSlot },
            { "Igniter", IgniterSlot },
            { "BatteryPack", BatteryPackSlot }
        };

    }

    // Update is called once per frame
    public void show()
    {
        inventoryPanel.gameObject.SetActive(true);
        
        foreach(KeyValuePair<string,GameObject> kvp in slots)
        {
            kvp.Value.gameObject.SetActive(false);
        }

        foreach (KeyValuePair<string,GameObject> kvp in player.inventory.Items)
        {
            slots[kvp.Key].gameObject.SetActive(true);
        }
    }

    public void hide()
    {
        inventoryPanel.gameObject.SetActive(false);
    }
}
