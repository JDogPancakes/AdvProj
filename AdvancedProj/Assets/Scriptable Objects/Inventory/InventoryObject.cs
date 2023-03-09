using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "ScriptableObjects/Inventory/Inventory")]
public class InventoryObject : ScriptableObject
{
    public WeaponObject weapon;
    public ArmourObject armour;
    public TrinketObject trinket;
    public ChipObject chip;
    public ConsumableObject consumable;

    public int inventoryLimit = 5;
    public List<ItemObject> container;

    private void Awake()
    {
        container = new List<ItemObject>();
    }

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
                weapon.name = itemPrefab.name;
                return true;
            }
        }
        else if (itemPrefab is ArmourObject)
        {
            Debug.Log("Picking up armour");
            if (!armour)
            {
                armour = Instantiate((ArmourObject)itemPrefab);
                armour.name = itemPrefab.name;
                return true;
            }
        }
        else if (itemPrefab is TrinketObject)
        {
            Debug.Log("Picking up trinket");
            if (!trinket)
            {
                trinket = Instantiate((TrinketObject)itemPrefab);
                trinket.name = itemPrefab.name;
                return true;
            }
        }
        else if (itemPrefab is ChipObject)
        {
            Debug.Log("Picking up chip");
            if (!chip)
            {
                chip = Instantiate((ChipObject)itemPrefab);
                chip.name = itemPrefab.name;
                return true;
            }
        }
        else if (itemPrefab is ConsumableObject)
        {
            Debug.Log(itemPrefab.GetType());
            Debug.Log("Picking up consumable");
            if (!consumable)
            {
                consumable = Instantiate((ConsumableObject)itemPrefab);
                consumable.name = itemPrefab.name;
            } else if (consumable.name.Equals(itemPrefab.name))
            {
                return consumable.add();
            }
        }
            return addToContainer(itemPrefab);
    }

    bool addToContainer(ItemObject itemPrefab)
    {
        if(itemPrefab is ConsumableObject)
        {
            Debug.Log("Adding Consumable to inventory");

            if (container.Exists(item => { return item.name == itemPrefab.name; }))
            {
                Debug.Log("Consumable Added");
                return ((ConsumableObject)container.Find(item =>{ return item.name == itemPrefab.name; })).add();
            } else if(container.Count < inventoryLimit)
            {
                container.Add(Instantiate(itemPrefab));
                return true;
            }
            return false;
        } else
        {
            Debug.Log("Adding Non-Consumable to inventory");
            if(container.Count < inventoryLimit)
            {
                container.Add(Instantiate(itemPrefab));
                Debug.Log("Item Added");
                return true;
            }
        }
        return false;
    }

    public void Clear()
    {
        weapon = null;
        armour = null;
        trinket = null;
        chip = null;
        consumable = null;
        container = null;
    }
}
