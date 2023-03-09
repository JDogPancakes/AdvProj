using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject weaponSlot, armourSlot, trinketSlot, chipSlot, consumableSlot, inventoryPanel, inventorySlotPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void CreateDisplay()
    {

    }

    void UpdateDisplay()
    {
        if (weaponSlot.GetComponentInChildren<UnityEngine.UI.Image>())
        {

            if (inventory.weapon)
            {
                if (inventory.weapon.reloading) weaponSlot.GetComponentInChildren<TextMeshProUGUI>().text = inventory.weapon.DisplayReloading();
                else weaponSlot.GetComponentInChildren<TextMeshProUGUI>().text = inventory.weapon.Display();
                weaponSlot.GetComponentInChildren<UnityEngine.UI.Image>().sprite = inventory.weapon.sprite;
                weaponSlot.GetComponentInChildren<UnityEngine.UI.Image>().color = new Color(255f, 255f, 255f);
            }
            else
            {
                weaponSlot.GetComponentInChildren<TextMeshProUGUI>().text = "No Weapon";
                weaponSlot.GetComponentInChildren<UnityEngine.UI.Image>().sprite = null;
                weaponSlot.GetComponentInChildren<UnityEngine.UI.Image>().color = new Color(77f / 255f, 77f / 255f, 77f / 255f);
            }
        }
        if (armourSlot.GetComponentInChildren<UnityEngine.UI.Image>())
        {

            if (inventory.armour)
            {
                armourSlot.GetComponentInChildren<TextMeshProUGUI>().text = inventory.armour.name;
                armourSlot.GetComponentInChildren<UnityEngine.UI.Image>().sprite = inventory.armour.sprite;
                armourSlot.GetComponentInChildren<UnityEngine.UI.Image>().color = new Color(255f, 255f, 255f);
            }
            else
            {
                armourSlot.GetComponentInChildren<TextMeshProUGUI>().text = "No Armour";
                armourSlot.GetComponentInChildren<UnityEngine.UI.Image>().sprite = null;
                armourSlot.GetComponentInChildren<UnityEngine.UI.Image>().color = new Color(77f / 255f, 77f / 255f, 77f / 255f);
            }
        }
        if (trinketSlot.GetComponentInChildren<UnityEngine.UI.Image>())
        {

            if (inventory.trinket)
            {
                trinketSlot.GetComponentInChildren<TextMeshProUGUI>().text = inventory.trinket.name;
                trinketSlot.GetComponentInChildren<UnityEngine.UI.Image>().sprite = inventory.trinket.sprite;
                trinketSlot.GetComponentInChildren<UnityEngine.UI.Image>().color = new Color(255f, 255f, 255f);
            }
            else
            {
                trinketSlot.GetComponentInChildren<TextMeshProUGUI>().text = "No Trinket";
                trinketSlot.GetComponentInChildren<UnityEngine.UI.Image>().sprite = null;
                trinketSlot.GetComponentInChildren<UnityEngine.UI.Image>().color = new Color(77f / 255f, 77f / 255f, 77f / 255f);
            }
        }
        if (chipSlot.GetComponentInChildren<UnityEngine.UI.Image>())
        {

            if (inventory.chip)
            {
                chipSlot.GetComponentInChildren<TextMeshProUGUI>().text = inventory.chip.name;
                chipSlot.GetComponentInChildren<UnityEngine.UI.Image>().sprite = inventory.chip.sprite;
                chipSlot.GetComponentInChildren<UnityEngine.UI.Image>().color = new Color(255f, 255f, 255f);
            }
            else
            {
                chipSlot.GetComponentInChildren<TextMeshProUGUI>().text = "No Chip";
                chipSlot.GetComponentInChildren<UnityEngine.UI.Image>().sprite = null;
                chipSlot.GetComponentInChildren<UnityEngine.UI.Image>().color = new Color(77f / 255f, 77f / 255f, 77f / 255f);
            }
        }
        if (consumableSlot.GetComponentInChildren<UnityEngine.UI.Image>())
        {

            if (inventory.consumable)
            {
                consumableSlot.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("{0}: {1}/{2}", inventory.consumable.name, inventory.consumable.quantity, inventory.consumable.maxQuantity);
                consumableSlot.GetComponentInChildren<UnityEngine.UI.Image>().sprite = inventory.consumable.sprite;
                consumableSlot.GetComponentInChildren<UnityEngine.UI.Image>().color = new Color(255f, 255f, 255f);
            }
            else
            {
                consumableSlot.GetComponentInChildren<TextMeshProUGUI>().text = "No Consumable";
                consumableSlot.GetComponentInChildren<UnityEngine.UI.Image>().sprite = null;
                consumableSlot.GetComponentInChildren<UnityEngine.UI.Image>().color = new Color(77f / 255f, 77f / 255f, 77f / 255f);
            }
        }
    }
}
