using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Toy Gun", menuName = "ScriptableObjects/Weapons/Toy Gun")]
public class ToyGunObject : WeaponObject
{
    new public void Awake()
    {
        base.Awake();
        ammo = 10;
        maxAmmo = 10;
        reloadSeconds = 2f;
        attackDelay = 0.5f;
        canAttack = true;
        reloading = false;
    }
    
    override public IEnumerator Attack(PlayerController player, float angle)
    {
        //if there's ammo & the last attack was long enough ago
        if (ammo > 0 && canAttack)
        {
            canAttack = false;
            //calculate angle & fire
            Quaternion qt = new Quaternion();
            qt.eulerAngles = new Vector3(0, 0, angle);
            player.SendMessage("SpawnBulletClientRpc", qt);
            ammo--;
            
            //cooldown before next attack
            yield return new WaitForSeconds(attackDelay);
            canAttack = true;
        }
    }

    public override IEnumerator Reload(InventoryItem weaponItem)
    {
        if (!reloading)
        {
            reloading = true;
            ammo = 0; //so you can't fire while reloading
            weaponItem.UpdateItem();
            yield return new WaitForSeconds(reloadSeconds);
            ammo = maxAmmo;
            weaponItem.UpdateItem();
            reloading = false;
        }
    }
}
