using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponObject : ItemObject
{
    [SerializeField]
    public int ammo ;
    public int maxAmmo;
    public float reloadSeconds;
    public float attackDelay;
    public bool canAttack;
    
    public void Awake()
    {
        type = ItemType.Weapon;
    }

    /**
     * Attack() and Reload() should be called via StartCoroutine(Attack()/Reload()), NOT DIRECTLY
     */
    public abstract IEnumerator Attack(Transform firepoint, float angle);
    public abstract IEnumerator Reload();
}
