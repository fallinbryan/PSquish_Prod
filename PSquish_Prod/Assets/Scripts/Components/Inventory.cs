using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProfessorSquish.Components
{
    public class Inventory
    {
        public Dictionary<string, GameObject> Items;

        public Inventory()
        {
            Items = new Dictionary<string, GameObject>();
        }

        public bool AddItem(GameObject item)
        {
            bool rv;
            try
            {
                Items.Add(item.tag, item);
                rv = true;
            }
            catch (System.ArgumentException e)
            {
                rv = false;
                Debug.Log(e);
            }

            return rv;
        }
    }
}
