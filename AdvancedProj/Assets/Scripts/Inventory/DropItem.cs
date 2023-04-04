using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropItem : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem dropped = eventData.pointerDrag.GetComponent<InventoryItem>();

    }

    [ServerRpc]
    private void SpawnItemServerRpc()
    {

    }
}
