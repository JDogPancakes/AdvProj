using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossController : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shieldTurrets;
    public GameObject[] childrens;


    private Transform player;
    public BossHPSlider hpSlider;

    [SerializeField]
    public float maxHP = 10;

    public float hp;
    private bool isShielded, hasBeenShielded = false;

    private bool canAttack = true;

    public float attackCooldown = 1f;
    private float numShieldTurrets;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hp = maxHP;
        numShieldTurrets = childrens.Length + 1;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(TryAttack());
    }

    public void Damage(int dmg)
    {
        if (!isShielded)
        {
            hp -= dmg;
            hpSlider.SetHp(hp);

            if (!hasBeenShielded && hp <= (maxHP / 2))
            {
                hasBeenShielded = isShielded = true;
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
            int selectedAttack = Random.Range(0, 6);
            if (selectedAttack < 3)
            {
                yield return LightAttack();
            }
            else if (selectedAttack < 5)
            {
                yield return MediumAttack();
            }
            else
            {
                yield return HeavyAttack();
            }
            canAttack = true;
        }
    }

    private IEnumerator LightAttack()
    {
        for (int bulletCount = Random.Range(3, 6); bulletCount > 0; bulletCount--)
        {
            Vector2 targetDir = (player.position - transform.position).normalized;
            float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f) % 360;

            Quaternion bulletDirection = new Quaternion();
            bulletDirection.eulerAngles = new Vector3(0, 0, angle);

            GameObject currentBullet = Instantiate(bullet, transform.position, bulletDirection);
            currentBullet.layer = 10;

            yield return new WaitForSeconds(0.25f);
        }
    }

    private IEnumerator MediumAttack()
    {
        int bulletCount = 7;

        //calculate angle to target -45 degrees
        Vector2 targetDir = (player.position - transform.position).normalized;
        float angleMod = (Random.Range(0, 2) == 1) ? -10 : 10;
        float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f - (angleMod*(bulletCount/2))) % 360;
        for (int i = 0; i < bulletCount; i++)
        {
            Quaternion bulletDirection = new Quaternion();
            bulletDirection.eulerAngles = new Vector3(0, 0, angle);

            GameObject currentBullet = Instantiate(bullet, transform.position, bulletDirection);
            currentBullet.layer = 10;
            angle = (angle + angleMod) % 360;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator HeavyAttack()
    {
        int numWaves = 3;
        int bulletsPerWave = 24;

        Vector2 targetDir = (player.position - transform.position).normalized;
        float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f) % 360;
        for (int i = 0; i < numWaves; i++)
        {
            yield return new WaitForSeconds(1);
            for (int j = 0; j < bulletsPerWave; j++)
            {
                Quaternion bulletDirection = new Quaternion();
                bulletDirection.eulerAngles = new Vector3(0, 0, angle);
                Instantiate(bullet, transform.position, bulletDirection).layer = 10;
                angle = (angle + (360 / bulletsPerWave)) % 360;
            }
            angle = (angle + 10) % 360;
        }
    }

    private void SpawnShieldTurrets()
    {
        foreach (GameObject child in childrens)
        {
            child.gameObject.SetActive(true);
        }
    }
    public void ShieldTurretDied()
    {
        numShieldTurrets--;
        if (numShieldTurrets <= 0)
        {
            isShielded = false;
        }
    }
}
