using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Toy Gun", menuName = "ScriptableObjects/Weapons/Toy Gun")]
public class ToyGunObject : WeaponObject
{
    new public void Awake()
    {
        base.Awake();
        ammo = maxAmmo = 10;
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
            //max inaccuracy of 5 degrees each way
            qt.eulerAngles = new Vector3(0, 0, angle + Random.Range(-5f, 5f));
            ammo--;
            player.SpawnBulletClientRpc(qt);
            
            //cooldown before next attack
            yield return new WaitForSeconds(attackDelay);
            canAttack = true;
        }
    }
}
