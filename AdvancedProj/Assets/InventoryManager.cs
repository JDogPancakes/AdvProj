using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    public bool AddItem(ItemObject itemPrefab)
    {
        //equip item if possible
        if (itemPrefab is WeaponObject && inventorySlots[0].GetComponentInChildren<InventoryItem>() == null)
        {
            WeaponObject newWeapon = (WeaponObject)Instantiate(itemPrefab);
            newWeapon.name = itemPrefab.name;
            SpawnItem(newWeapon, inventorySlots[0]);
            inventorySlots[0].GetComponentInChildren<InventoryItem>().currentItem = newWeapon;
            return true;
        }
        else if (itemPrefab is ArmourObject && inventorySlots[1].GetComponentInChildren<InventoryItem>() == null)
        {
            ArmourObject newArmour = (ArmourObject)Instantiate(itemPrefab);
            newArmour.name = itemPrefab.name;
            SpawnItem(newArmour, inventorySlots[1]);
            inventorySlots[1].GetComponentInChildren<InventoryItem>().currentItem = newArmour;
            return true;
        }
        else if (itemPrefab is TrinketObject && inventorySlots[2].GetComponentInChildren<InventoryItem>() == null)
        {
            TrinketObject newTrinket = (TrinketObject)Instantiate(itemPrefab);
            newTrinket.name = itemPrefab.name;
            SpawnItem(newTrinket, inventorySlots[2]);
            inventorySlots[2].GetComponentInChildren<InventoryItem>().currentItem = newTrinket;
            return true;
        }
        else if (itemPrefab is ChipObject && inventorySlots[3].GetComponentInChildren<InventoryItem>() == null)
        {
            ChipObject newChip = (ChipObject)Instantiate(itemPrefab);
            newChip.name = itemPrefab.name;
            SpawnItem(newChip, inventorySlots[3]);
            inventorySlots[3].GetComponentInChildren<InventoryItem>().currentItem = newChip;
            return true;
        }
        else if (itemPrefab is ConsumableObject)
        {
            Debug.Log("Picking up consumable");
            Debug.Log(itemPrefab.name);
            if (inventorySlots[4].GetComponentInChildren<InventoryItem>() == null)
            {
                //create new item if slot is empty
                ConsumableObject newConsumable = (ConsumableObject)Instantiate(itemPrefab);
                newConsumable.name = itemPrefab.name;
                newConsumable.add();
                Debug.Log(newConsumable.Display());
                SpawnItem(newConsumable, inventorySlots[4]);
                inventorySlots[4].GetComponentInChildren<InventoryItem>().currentItem = newConsumable;
                return true;
            }
            else if (inventorySlots[4].GetComponentInChildren<InventoryItem>().currentItem.name.Equals(itemPrefab.name))
            {
                //Increase stack count if possible
                if (((ConsumableObject)inventorySlots[4].GetComponentInChildren<InventoryItem>().currentItem).add())
                {
                    inventorySlots[4].GetComponentInChildren<InventoryItem>().UpdateItem();
                    return true;
                }
            }


        }

        //Store item if it couldn't be equipped
        for (int i = 5; i < inventorySlots.Length; i++)
        {
            ItemObject newItem = Instantiate(itemPrefab);
            newItem.name = itemPrefab.name;
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

            if (itemInSlot == null)
            {
                if (newItem is ConsumableObject)
                {
                    Debug.Log("Adding Consumable to Inventory");
                    ((ConsumableObject)newItem).add();
                }
                SpawnItem(newItem, slot);
                return true;
            }
            else if (newItem is ConsumableObject && itemInSlot.name.Equals(newItem.name))
            {
                if (((ConsumableObject)newItem).add()) return true;
            }
        }
        //return false if item couldn't be equipped or stored
        return false;
    }
    public WeaponObject getWeapon()
    {
        return (WeaponObject)inventorySlots[0].GetComponentInChildren<InventoryItem>().currentItem;
    }

    public ArmourObject getArmour()
    {
        return (ArmourObject)inventorySlots[1].GetComponentInChildren<InventoryItem>().currentItem;
    }
    public TrinketObject getTrinket()
    {
        return (TrinketObject)inventorySlots[2].GetComponentInChildren<InventoryItem>().currentItem;
    }
    public ChipObject getChip()
    {
        return (ChipObject)inventorySlots[3].GetComponentInChildren<InventoryItem>().currentItem;
    }
    public ConsumableObject getConsumable()
    {
        return (ConsumableObject)inventorySlots[4].GetComponentInChildren<InventoryItem>().currentItem;
    }


    public bool hasWeapon()
    {
        return inventorySlots[0].GetComponentInChildren<InventoryItem>() != null;
    }
    public bool hasArmour()
    {
        return inventorySlots[1].GetComponentInChildren<InventoryItem>() != null;
    }
    public bool hasTrinket()
    {
        return inventorySlots[2].GetComponentInChildren<InventoryItem>() != null;
    }
    public bool hasChip()
    {
        return inventorySlots[3].GetComponentInChildren<InventoryItem>() != null;
    }
    public bool hasConsumable()
    {
        return inventorySlots[4].GetComponentInChildren<InventoryItem>() != null;
    }

    public void SpawnItem(ItemObject item, InventorySlot slot)
    {
        Debug.LogFormat("Spawning {0} in inventory", item.name);
        GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGO.GetComponent<InventoryItem>();
        inventoryItem.InitializeItem(item);
    }
}
