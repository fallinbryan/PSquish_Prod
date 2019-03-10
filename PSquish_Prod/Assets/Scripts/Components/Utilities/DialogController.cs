using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProfessorSquish.Components.Utilities
{

    public class DialogController : MonoBehaviour
    {

        [SerializeField]
        public GameObject DialogPanel;

        public void Dialog(string message)
        {
            GameObject dialog = Instantiate(DialogPanel, GameObject.Find("Canvas").transform);
            dialog.gameObject.GetComponentInChildren<Text>().text = message;

        }

        public void close()
        {
            Destroy(this.gameObject);
        }
    }
}