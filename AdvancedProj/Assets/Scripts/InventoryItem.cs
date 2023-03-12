using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public ItemObject currentItem;
    [HideInInspector] public Transform parentAfterDrag;

    public void InitializeItem(ItemObject newItem)
    {
        currentItem = newItem;
        UpdateItem();
    }

    public void UpdateItem()
    {
        transform.SetAsFirstSibling();
        image.color = new Color(255, 255, 255, 255);
        image.sprite = currentItem.sprite;
        transform.parent.GetComponentInChildren<TextMeshProUGUI>().text = currentItem.Display();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root.GetComponentInChildren<Canvas>().transform);
        parentAfterDrag.GetComponent<Image>().raycastTarget = false;
        image.raycastTarget = false;
        transform.position = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
        transform.localPosition = new Vector3(0, 0, 0);
        UpdateItem();
    }
}
