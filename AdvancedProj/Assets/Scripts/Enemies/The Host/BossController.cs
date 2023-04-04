using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BossController : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public GameObject shieldTurrets;


    private List<GameObject> players;

    public float maxHP = 10;

    public float hp;
    private bool isShielded, hasBeenShielded = false;

    public bool canAttack = false;


    public float attackCooldown = 1f;

    public AudioSource shooting;
    void Awake()
    {
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        hp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsServer) return;
        StartCoroutine(TryAttack());
    }


    [ServerRpc]
    public void DamageServerRpc(int dmg)
    {
        if (!isShielded)
        {
            hp -= dmg;

            if (!hasBeenShielded && hp <= (maxHP / 2))
            {
                hasBeenShielded = isShielded = true;
                GetComponent<SpriteRenderer>().color = Color.blue;
                SpawnShieldTurrets();
            }
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }

    }


    private IEnumerator TryAttack()
    {
        if (canAttack)
        {
            canAttack = false;
            yield return new WaitForSeconds(attackCooldown);

            GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
            if (targets.Length == 0) yield return null;
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

            int selectedAttack = Random.Range(0, 6);
            if (selectedAttack < 3)
            {
                yield return LightAttack(closestTarget.transform);
            }
            else if (selectedAttack < 5)
            {
                yield return MediumAttack(closestTarget.transform);
            }
            else
            {
                yield return HeavyAttack();
            }
            canAttack = true;
        }
    }

    private IEnumerator LightAttack(Transform player)
    {
        for (int bulletCount = Random.Range(3, 6); bulletCount > 0; bulletCount--)
        {
            Vector2 targetDir = (player.position - transform.position).normalized;
            float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f) % 360;

            SpawnBulletClientRpc(angle);

            yield return new WaitForSeconds(0.25f);
        }
    }

    private IEnumerator MediumAttack(Transform player)
    {
        int bulletCount = 7;

        Vector2 targetDir = (player.position - transform.position).normalized;

        //calculate angle to target -45 degrees
        float angleMod = (Random.Range(0, 2) == 1) ? -10 : 10;
        float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f - (angleMod * (bulletCount / 2))) % 360;
        for (int i = 0; i < bulletCount; i++)
        {
            SpawnBulletClientRpc(angle);
            angle = (angle + angleMod) % 360;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator HeavyAttack()
    {
        int numWaves = 3;
        int bulletsPerWave = 24;

        float angle = 0;
        for (int i = 0; i < numWaves; i++)
        {
            yield return new WaitForSeconds(1);
            for (int j = 0; j < bulletsPerWave; j++)
            {
                SpawnBulletClientRpc(angle);
                angle = (angle + (360 / bulletsPerWave)) % 360;
            }
            angle = (angle + 10) % 360;
        }
    }

    [ClientRpc]
    private void SpawnBulletClientRpc(float angle)
    {
        Quaternion bulletDirection = new Quaternion();
        bulletDirection.eulerAngles = new Vector3(0, 0, angle);
        Instantiate(bulletPrefab, transform.position, bulletDirection).layer = 10;
        shooting.Play();
    }

    private void SpawnShieldTurrets()
    {
        for (int i = 0; i < shieldTurrets.transform.childCount; i++)
        {
            shieldTurrets.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    public IEnumerator ShieldTurretDied()
    {
        yield return new WaitForFixedUpdate();
        if (shieldTurrets.transform.childCount == 0)
        {
            isShielded = false;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
