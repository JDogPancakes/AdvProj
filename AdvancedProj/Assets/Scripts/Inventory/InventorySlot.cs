using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    List<ItemType> allowedItemTypes;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem dropped = eventData.pointerDrag.GetComponent<InventoryItem>();
        //if the dropped item is allowed in the current slot
        if (allowedItemTypes.Contains(dropped.currentItem.type))
        {
            //if the current slot only has text (no item)
            if (transform.childCount == 1)
            {
                dropped.parentAfterDrag.GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
                dropped.parentAfterDrag.GetComponent<Image>().raycastTarget = true;
                dropped.parentAfterDrag = transform;
            }
            //if the current slot has text and InventoryItem
            else if (transform.childCount == 2)
            {
                // if the item in the current slot is allowed in the slot of the dropped item
                if (dropped.parentAfterDrag.GetComponent<InventorySlot>().allowedItemTypes.Contains(GetComponentInChildren<InventoryItem>().currentItem.type))
                {
                    //if item is consumable, combine stack
                    if (dropped.currentItem is ConsumableObject
                        && dropped.currentItem.name.Equals(GetComponentInChildren<InventoryItem>().currentItem.name))
                    {
                        while (((ConsumableObject)dropped.currentItem).quantity > 0 && ((ConsumableObject)GetComponentInChildren<InventoryItem>().currentItem).hasSpace())
                        {
                            ((ConsumableObject)GetComponentInChildren<InventoryItem>().currentItem).add();
                            ((ConsumableObject)dropped.currentItem).quantity--;
                        }
                        if(((ConsumableObject)dropped.currentItem).quantity <= 0)
                        {
                            dropped.currentItem = null;
                            dropped.transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = "Empty";
                            Destroy(dropped.gameObject);
                        }
                        GetComponentInChildren<InventoryItem>().UpdateItem();
                    }
                    else
                    {
                        //swap items
                        ItemObject temp = dropped.currentItem;
                        dropped.currentItem = GetComponentInChildren<InventoryItem>().currentItem;
                        GetComponentInChildren<InventoryItem>().InitializeItem(temp);
                    }
                }
            }
        }
    }
}
