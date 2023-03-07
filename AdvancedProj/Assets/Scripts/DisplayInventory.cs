using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayInventory : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject weapon, armour, trinket, chip, consumable;
    // Start is called before the first frame update
    void Start()
    {
        weapon.GetComponentInChildren<TextMeshProUGUI>().text = "No Weapon";
        armour.GetComponentInChildren<TextMeshProUGUI>().text = "No Armour";
        trinket.GetComponentInChildren<TextMeshProUGUI>().text = "No Trinket";
        chip.GetComponentInChildren<TextMeshProUGUI>().text = "No Chip";
        consumable.GetComponentInChildren<TextMeshProUGUI>().text = "No Consumable";
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.weapon)
        {
            if (inventory.weapon.reloading) weapon.GetComponentInChildren<TextMeshProUGUI>().text = inventory.weapon.DisplayReloading();
            else weapon.GetComponentInChildren<TextMeshProUGUI>().text = inventory.weapon.Display();
            weapon.GetComponent<UnityEngine.UI.Image>().sprite = inventory.weapon.sprite;
            weapon.GetComponent<UnityEngine.UI.Image>().color = new Color(255f, 255f, 255f);
        }
        else
        {
            weapon.GetComponentInChildren<TextMeshProUGUI>().text = "No Weapon";
            weapon.GetComponent<UnityEngine.UI.Image>().sprite = null;
            weapon.GetComponent<UnityEngine.UI.Image>().color = new Color(77f / 255f, 77f / 255f, 77f / 255f);
        }
        if (inventory.armour)
        {
            armour.GetComponentInChildren<TextMeshProUGUI>().text = inventory.armour.name;
            armour.GetComponent<UnityEngine.UI.Image>().sprite = inventory.armour.sprite;
            armour.GetComponent<UnityEngine.UI.Image>().color = new Color(255f, 255f, 255f);
        }
        else
        {
            armour.GetComponentInChildren<TextMeshProUGUI>().text = "No Armour";
            armour.GetComponent<UnityEngine.UI.Image>().sprite = null;
            armour.GetComponent<UnityEngine.UI.Image>().color = new Color(77f / 255f, 77f / 255f, 77f / 255f);
        }

        if (inventory.trinket)
        {
            trinket.GetComponentInChildren<TextMeshProUGUI>().text = inventory.trinket.name;
            trinket.GetComponent<UnityEngine.UI.Image>().sprite = inventory.trinket.sprite;
            trinket.GetComponent<UnityEngine.UI.Image>().color = new Color(255f, 255f, 255f);
        }
        else
        {
            trinket.GetComponentInChildren<TextMeshProUGUI>().text = "No Trinket";
            trinket.GetComponent<UnityEngine.UI.Image>().sprite = null;
            trinket.GetComponent<UnityEngine.UI.Image>().color = new Color(77f / 255f, 77f / 255f, 77f / 255f);
        }

        if (inventory.chip)
        {
            chip.GetComponentInChildren<TextMeshProUGUI>().text = inventory.chip.name;
            chip.GetComponent<UnityEngine.UI.Image>().sprite = inventory.chip.sprite;
            chip.GetComponent<UnityEngine.UI.Image>().color = new Color(255f, 255f, 255f);
        }
        else
        {
            chip.GetComponentInChildren<TextMeshProUGUI>().text = "No Chip";
            chip.GetComponent<UnityEngine.UI.Image>().sprite = null;
            chip.GetComponent<UnityEngine.UI.Image>().color = new Color(77f / 255f, 77f / 255f, 77f / 255f);
        }

        if (inventory.consumable)
        {
            consumable.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("{0}: {1}/{2}", inventory.consumable.name, inventory.consumable.quantity, inventory.consumable.maxQuantity);
            consumable.GetComponent<UnityEngine.UI.Image>().sprite = inventory.consumable.sprite;
            consumable.GetComponent<UnityEngine.UI.Image>().color = new Color(255f, 255f, 255f);
        }
        else
        {
            consumable.GetComponentInChildren<TextMeshProUGUI>().text = "No Consumable";
            consumable.GetComponent<UnityEngine.UI.Image>().sprite = null;
            consumable.GetComponent<UnityEngine.UI.Image>().color = new Color(77f / 255f, 77f / 255f, 77f / 255f);
        }
    }
}
