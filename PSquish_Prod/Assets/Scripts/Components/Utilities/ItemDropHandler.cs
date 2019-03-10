using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ProfessorSquish.Characters.Player;

namespace ProfessorSquish.Components.Utilities
{
    public class ItemDropHandler : MonoBehaviour, IDropHandler
    {




        public GameObject item
        {
            get
            {
                if (transform.childCount > 0)
                {
                    return transform.GetChild(0).gameObject;
                }
                return null;
            }
        }

        public void OnDrop(PointerEventData eventData)
        {

            //if (!item)
            //{

            //    if (this.tag == "ISlot" && ItemDragHandler.draggedItem.tag == "Weapon")
            //    {
            //        GameObject.Find("Player").GetComponent<PlayerController>().inventory.Items[ItemDragHandler.draggedItem.name] = new Squish.Weapon(ItemDragHandler.draggedItem.name);
                   ItemDragHandler.draggedItem.transform.SetParent(this.transform);
            //        return;
            //    }

            //    if (ItemDragHandler.draggedItem.tag == "Weapon" && this.tag == "WeaonSlot")
            //    {
            //        foreach (GameObject slot in GameObject.FindGameObjectsWithTag("PartSlot"))
            //        {
            //            if (slot.transform.childCount > 0)
            //            {
            //                return;
            //            }
            //        }
            //    }

            //    if (ItemDragHandler.draggedItem.tag == "Weapon" && this.tag != "WeaonSlot") // <-- yes, I know.  Weaon..  I'm too much of hurry to take it off everything already tagged, so it lives
            //    {
            //        return;
            //    }

            //    if (this.tag == "WeaonSlot" && ItemDragHandler.draggedItem.tag != "Weapon")
            //    {
            //        return;
            //    }


            //    ItemDragHandler.draggedItem.transform.SetParent(this.transform);

            //    if (this.name == "ArmedWeaponPanel")
            //    {
            //        GameObject.Find("PlayerN").GetComponent<PlayerController>().Inventory.Remove(ItemDragHandler.draggedItem.name);
            //        GameObject.Find("PlayerN").GetComponent<PlayerController>().playerWeapon = new Squish.Weapon(ItemDragHandler.draggedItem.name);

            //    }



            //}

        }

    }
}