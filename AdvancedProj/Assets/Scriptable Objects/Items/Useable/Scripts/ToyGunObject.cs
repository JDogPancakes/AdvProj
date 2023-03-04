using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToyGunObject : WeaponObject
{
    bool reloading = false;
    public GameObject bulletPrefab;
    private void OnEnable()
    {
        ammo = 10;
        maxAmmo = 10;
        reloadSeconds = 2;
        attackDelay = 0.5f;
        canAttack = true;
    }

    override public IEnumerator Attack(Transform firepoint, float angle)
    {
        //if there's ammo & the last attack was long enough ago
        if (ammo > 0 && canAttack)
        {
            canAttack = false;
            //calculate angle & fire
            Quaternion qt = new Quaternion();
            qt.eulerAngles = new Vector3(0, 0, angle);
            Instantiate(bulletPrefab, firepoint.position, qt);
            ammo--;
            
            //cooldown before next attack
            yield return new WaitForSeconds(attackDelay);
            canAttack = true;
        }
    }

    public override IEnumerator Reload()
    {
        if (!reloading)
        {
            reloading = true;
            ammo = 0; //so you can't fire while reloading
            yield return new WaitForSeconds(reloadSeconds);
            ammo = maxAmmo;
            reloading = false;
        }
    }
}