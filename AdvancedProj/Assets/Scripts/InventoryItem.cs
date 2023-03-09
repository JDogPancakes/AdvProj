using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    public TextMeshProUGUI text;
    [HideInInspector] public ItemObject currentItem;
    [HideInInspector] public Transform parentAfterDrag;

    public void InitializeItem(ItemObject newItem)
    {
        currentItem = newItem;
        UpdateItem();
    }
    public void UpdateItem()
    {
        initText();
        transform.SetAsFirstSibling();
        image.color = new Color(255, 255, 255, 255);
        image.sprite = currentItem.sprite;
        text.text = currentItem.Display();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.parent.parent);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        transform.position = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        transform.SetParent(parentAfterDrag);
        UpdateItem();
        image.raycastTarget = true;
    }

    private void initText()
    {
        text = transform.parent.gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }
}
