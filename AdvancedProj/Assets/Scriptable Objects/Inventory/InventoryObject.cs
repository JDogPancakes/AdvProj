using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "ScriptableObjects/Inventory/Inventory")]
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
     *return false if slot is already full
     *       true if item is added successfully
     */
    public bool addItem(ItemObject itemPrefab)

    {
        if (itemPrefab is WeaponObject)
        {
            Debug.Log("Picking up weapon");
            if (!weapon)
            {
                weapon = Instantiate((WeaponObject)itemPrefab);
                return true;
            }
            return false;
        }
        else if (itemPrefab is ArmourObject)
        {
            Debug.Log("Picking up armour");
            if (!armour)
            {
                armour = Instantiate((ArmourObject)itemPrefab);
                return true;
            }
            return false;
        }
        else if (itemPrefab is TrinketObject)
        {
            Debug.Log("Picking up trinket");
            if (!trinket)
            {
                trinket = Instantiate((TrinketObject)itemPrefab);
                return true;
            }
            return false;
        }
        else if (itemPrefab is ChipObject)
        {
            Debug.Log("Picking up chip");
            if (!chip)
            {
                chip = Instantiate((ChipObject)itemPrefab);
                return true;
            }
            return false;
        }
        else if (itemPrefab is ConsumableObject)
        {
            Debug.Log("Picking up consumable");
            if (!consumable)
            {
                consumable = Instantiate((ConsumableObject)itemPrefab);
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
