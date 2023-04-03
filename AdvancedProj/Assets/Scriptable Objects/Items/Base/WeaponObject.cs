using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


public abstract class WeaponObject : ItemObject
{
    public GameObject bulletPrefab;
    public int ammo;
    public int maxAmmo;
    public float reloadSeconds;
    public float attackDelay;
    public bool canAttack;
    public bool reloading;

    public void Awake()
    {
        type = ItemType.Weapon;
    }

    /**
     * Attack() and Reload() should be called via StartCoroutine(Attack()/Reload()), NOT DIRECTLY
     */
    public abstract IEnumerator Attack(PlayerController player, float angle);
    public virtual IEnumerator Reload(InventoryItem weaponItem)
    {
        if (!reloading)
        {
            reloading = true;
            ammo = 0; //so you can't fire while reloading
            weaponItem.UpdateItem();
            yield return new WaitForSeconds(reloadSeconds);
            ammo = maxAmmo;
            reloading = false;
            weaponItem.UpdateItem();
        }
    }
    public override string Display()
    {
        if (reloading) return DisplayReloading();
        return string.Format("{0}: {1}/{2}", name, ammo, maxAmmo);
    }
    public virtual string DisplayReloading()
    {
        return string.Format("{0}: Reloading", name);
    }
}
