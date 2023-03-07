using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class turretScript : MonoBehaviour
{
    public Transform target;
    public GameObject bulletPrefab;
    public bool canAttack = true, reloading = false;
    public Transform firepoint;

    public int hp;
    public int currentAmmo = 3, maxAmmo = 3;
    public float reloadDelay = 2f;
    public float attackDelay;

    // Start is called before the first frame update
    void Start()
    {
        attackDelay = 0.1f;
        hp = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            return;
        }
        Vector2 targetDir = target.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
        transform.eulerAngles = new Vector3(0, 0, angle);
        Shoot();
    }

    void Shoot()
    {
        Vector2 shootDir = target.position - transform.position;
        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg - 90f;
        StartCoroutine(Attack(firepoint, angle));
    }

    public IEnumerator Attack(Transform firepoint, float angle)
    {
        //if there's ammo & the last attack was long enough ago
        if (currentAmmo > 0 && !reloading)
        {
            if (canAttack)
            {
                canAttack = false;
                //calculate angle & fire
                Quaternion qt = new Quaternion();
                qt.eulerAngles = new Vector3(0, 0, angle);
                GameObject currentBullet = Instantiate(bulletPrefab, firepoint.position, qt);
                currentBullet.layer = 10;
                currentAmmo--;

                //cooldown before next attack
                yield return new WaitForSeconds(attackDelay);
                canAttack = true;
            }
        }
        else
        {
            
            StartCoroutine(Reload());
        }
    }

    public IEnumerator Reload()
    {
        if (!reloading)
        {
            reloading = true;
            yield return new WaitForSeconds(reloadDelay);
            currentAmmo = maxAmmo;
            reloading = false;
        }
    }

    public void Damage(int dmg)
    {
        hp--;
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
