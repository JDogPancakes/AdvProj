using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Burst Rifle", menuName = "ScriptableObjects/Weapons/Burst Rifle")]
public class BurstRifleObject : WeaponObject
{
    public float bulletsPerBurst, secondsPerBurst;

    new public void Awake()
    {
        base.Awake();
        ammo = maxAmmo = 30;
        reloadSeconds = 2.5f;
        attackDelay = 0.5f;
        bulletsPerBurst = 3;
        secondsPerBurst = 0.5f;

        canAttack = true;
        reloading = false;
    }

    public override IEnumerator Attack(PlayerController player, float angle)
    {
        if (ammo > 0 && canAttack)
        {
            canAttack = false;
            Quaternion qt = new Quaternion();

            for (int i = 0; i < bulletsPerBurst; i++)
            {
                //max inaccuracy of 2 degrees each way
                qt.eulerAngles = new Vector3(0, 0, angle + Random.Range(-2f, 2f));
                ammo--;
                player.SpawnBulletClientRpc(qt);
                if (ammo == 0) break;
                yield return new WaitForSeconds(secondsPerBurst / bulletsPerBurst);
            }
            yield return new WaitForSeconds(attackDelay);
            canAttack = true;
        }
    }
}
