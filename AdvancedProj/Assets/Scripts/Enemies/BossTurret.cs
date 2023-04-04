using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

public class BossTurret : NetworkBehaviour
{
    public GameObject bulletPrefab;
    private Door door;
    private bool canAttack = false;
    private bool reloading = false;
    public Transform firepoint;

    private int numPlayers = 0;
    public int hp;
    public int currentAmmo = 20, maxAmmo = 20;
    public float reloadDelay = 1f;
    public float attackDelay;

    private RaycastHit2D hit;
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        attackDelay = 0.1f;
        hp = 8;
        Physics2D.queriesStartInColliders = false;
    }


    private void FixedUpdate()
    {
        if (!IsServer) return;
        //get list of targets
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        if (targets.Length == 0) return;
        //find nearest target
        float minDistance = Mathf.Infinity;
        GameObject closestTarget = null;
        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestTarget = target;
            }
        }
        Vector2 targetDir = (closestTarget.transform.position - transform.position).normalized;

        Shoot(targetDir);

    }

    public void SetCanAttack(bool canAttack)
    {
        this.canAttack = canAttack;
    }

    void Shoot(Vector2 targetDir)
    {
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
        transform.eulerAngles = new Vector3(0, 0, angle);
        StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        //if there's ammo & the last attack was long enough ago
        if (currentAmmo > 0 && !reloading)
        {
            if (canAttack)
            {
                animator.SetTrigger("Shooting");
                canAttack = false;
                //calculate angle & fire
                for (currentAmmo = maxAmmo; currentAmmo > 0; currentAmmo--)
                {
                    SpawnBulletClientRpc();
                    //cooldown before next attack
                    yield return new WaitForSeconds(attackDelay);
                }
                canAttack = true;
            }
        }
        else
        {
            StartCoroutine(Reload());
        }
    }

    [ClientRpc]
    private void SpawnBulletClientRpc()
    {
        Quaternion offset = new Quaternion();
        offset.eulerAngles = new Vector3(0, 0, Random.Range(-20, 20));
        GameObject currentBullet = Instantiate(bulletPrefab, firepoint.position, transform.rotation * offset);
        currentBullet.GetComponent<Bullet>().ricochetsRemaining = 3;
        currentBullet.GetComponent<Bullet>().damage = 1;
        currentBullet.layer = 10;
    }

    public IEnumerator Reload()
    {
        if (!reloading)
        {
            animator.SetTrigger("Reloading");
            animator.ResetTrigger("Shooting");
            reloading = true;
            yield return new WaitForSeconds(reloadDelay);
            animator.ResetTrigger("Reloading");
            currentAmmo = maxAmmo;
            reloading = false;
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void DamageServerRpc(int dmg)
    {
        hp--;
        if (hp <= 0)
        {
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<NetworkObject>().Despawn();
            try
            {
                door = transform.parent.parent.GetComponentInChildren<Door>();
                door.EnemyDied(this.gameObject.transform.parent.gameObject);
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("Door Not Found");
            }
        }
    }
}

