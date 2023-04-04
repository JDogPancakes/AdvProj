using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Railgun", menuName ="ScriptableObjects/Weapons/Railgun")]
public class RailgunObject : WeaponObject
{
    new public void Awake()
    {
        base.Awake();
        ammo = maxAmmo = 1;
        reloadSeconds = 3;
        attackDelay = 0;
        canAttack = true;
        reloading = false;
    }
    public override IEnumerator Attack(PlayerController player, float angle)
    {
        if(canAttack && ammo > 0)
        {
            canAttack = false;
            ammo--;
            Quaternion qt = new Quaternion();
            //max inaccuracy of 5 degrees each way
            qt.eulerAngles = new Vector3(0, 0, angle);
            player.SpawnBulletClientRpc(qt, dmg:10, pierce: 1, speed: 30);
            yield return null;
            canAttack = true;
        }
    }
}
