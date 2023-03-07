using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shotgun", menuName = "ScriptableObjects/Weapons/Shotgun")]
public class ShotgunObject : WeaponObject
{
    public GameObject bulletPrefab;
    public int bulletCount = 5;
    public float spreadAngle = 7.5f;
    public bool cancelReload = false;
    private void OnEnable()
    {
        ammo = 5;
        maxAmmo = 5;
        reloadSeconds = 1f;
        attackDelay = 0.75f;
        canAttack = true;
        reloading = false;
    }
    public override IEnumerator Attack(Transform firepoint, float angle)
    {
        //if there's ammo & the last attack was long enough ago
        if (canAttack)
        {
            //cancel reload
            if (reloading)
            {
                cancelReload = true;
            }
            else if (ammo > 0)
            {
                //lock out attack
                canAttack = false;
                Debug.Log("Firing Shotgun");
                //fire bullets
                for (int i = 0; i < bulletCount; i++)
                {
                    Debug.LogFormat("Bullet {0}", i);
                    //calculate angle & fire
                    Quaternion qt = new Quaternion();
                    qt.eulerAngles = new Vector3(0, 0, angle + Random.Range(-spreadAngle, spreadAngle));
                    Instantiate(bulletPrefab, firepoint.position, qt);
                }
                ammo--;

                //cooldown before next attack
                yield return new WaitForSeconds(attackDelay);
                canAttack = true;
            }
        }
    }

    public override IEnumerator Reload()
    {
        if (!reloading)
        {
            //Lock out firing and reloading
            reloading = true;
            Debug.Log("Beginning Reload");

            //Loop to add 1 bullet at a time
            while (ammo < maxAmmo && reloading)
            {
                yield return new WaitForSeconds(reloadSeconds);
                if (reloading) ammo++;
                if (cancelReload)
                {
                    cancelReload = false;
                    break;
                }

            }
            reloading = false;
            Debug.Log("Reload Finished");
        }
    }

    public override string DisplayReloading()
    {
        return string.Format("{0}: Reloading {1}/{2}", name, ammo, maxAmmo);
    }
}
