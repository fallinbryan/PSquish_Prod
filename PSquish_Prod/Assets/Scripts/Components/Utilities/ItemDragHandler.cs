using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace ProfessorSquish.Components.Utilities
{
    public class ItemDragHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        public static GameObject draggedItem;
        private Vector2 originalPostion;
        private Transform beginParent;

        public void OnBeginDrag(PointerEventData eventData)
        {
            draggedItem = this.gameObject;
            beginParent = transform.parent;
            originalPostion = transform.position;
            GetComponents<CanvasGroup>()[0].blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {

            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            draggedItem = null;
            GetComponents<CanvasGroup>()[0].blocksRaycasts = true;
            //if (eventData.pointerCurrentRaycast.gameObject != GameObject.Find("BuildWeaponPanel"))
            if (transform.parent.transform == beginParent.transform)
            {
                //GetComponent<RectTransform>().anchoredPosition = originalPostion;
                transform.position = originalPostion;
            }
        }
    }
}