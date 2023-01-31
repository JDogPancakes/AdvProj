using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Inventory", menuName ="ScriptableObjects/Inventory/Inventory")]
public class InventoryObject : ScriptableObject
{
    [SerializeField]
    public WeaponObject weapon;
    [SerializeField]
    public ArmourObject armour;
    [SerializeField]
    public TrinketObject trinket;
    [SerializeField]
    public ChipObject chip;
    [SerializeField]
    public ConsumableObject consumable;
    
    /**add item to inventory
     *return false if item is too heavy to add
     *       true if item is added successfully
     */
    public bool addItem(ItemObject item)

    {
        if (item is WeaponObject)
        {
            Debug.Log("Picking up weapon");
            if (!weapon)
            {
                weapon = (WeaponObject) item;
                return true;
            }
            return false;
        } else if (item is ArmourObject)
        {
            Debug.Log("Picking up armour");
            if (!armour)
            {
                armour = (ArmourObject)item;
                return true;
            }
            return false;
        }
        else if(item is TrinketObject)
        {
            Debug.Log("Picking up trinket");
            if (!trinket)
            {
                trinket = (TrinketObject)item;
                return true;
            }
            return false;
        } else if (item is ChipObject)
        {
            Debug.Log("Picking up chip");
            if (!chip)
            {
                chip = (ChipObject)item;
                return true;
            }
            return false;
        }
        else if (item is ConsumableObject)
        {
            Debug.Log("Picking up consumable");
            if (!consumable)
            {
                consumable = (ConsumableObject)item;
                return true;
            }
            return false;
        }
        else
            return false;
    }

    public void Clear()
    {
        weapon = null;
        armour = null;
        trinket = null;
        chip = null;
        consumable = null;
    }
}
