using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    List<ItemType> allowedItemTypes;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem dropped = eventData.pointerDrag.GetComponent<InventoryItem>();
        if (allowedItemTypes.Contains(dropped.currentItem.type))
        {
            if (transform.childCount == 1)
            {
                dropped.text.text = "Empty";
                dropped.parentAfterDrag = transform;
            } else if (transform.childCount == 2 && gameObject.GetComponentInChildren<InventorySlot>().allowedItemTypes.Contains(dropped.currentItem.type))
            {

                ItemObject temp = dropped.currentItem;
                dropped.currentItem = gameObject.GetComponentInChildren<InventoryItem>().currentItem;
                gameObject.GetComponentInChildren<InventoryItem>().currentItem = temp;

                dropped.UpdateItem();
                gameObject.GetComponentInChildren<InventoryItem>().UpdateItem();
            }
        }
    }
}
