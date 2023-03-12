using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponObject : ItemObject
{
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
    public abstract IEnumerator Attack(Transform firepoint, float angle);
    public abstract IEnumerator Reload(InventoryItem weaponItem);
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
