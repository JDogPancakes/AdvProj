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
    private bool isShielded = false;

    private bool canAttack = true;
    private bool canBigAttack = true;

    private float numShieldTurrets;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hp = maxHP;
        numShieldTurrets= childrens.Length + 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(BasicAttack());
        StartCoroutine(BigAttackSwitch());
    }


    private IEnumerator BasicAttack()
    {
        if (canAttack)
        {
            canAttack= false;

            Vector2 targetDir = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;

            Quaternion qt = new Quaternion();
            qt.eulerAngles = new Vector3(0, 0, angle);

            GameObject currentBullet = Instantiate(bullet, transform.position, qt);
            currentBullet.layer = 10;

            yield return new WaitForSeconds(0.5f);
            canAttack= true;
        }
    }

    private IEnumerator BigAttackSwitch()
    {
        if (canBigAttack)
        {
            canBigAttack= false;
            int randint = Random.Range(0, 1);

            switch (randint)
            {
                case 0:
                    BigAttack();
                    break;
            }
            yield return new WaitForSeconds(5f);
            canBigAttack= true;
        }

    }
    public void Damage(int dmg)
    {
        if (!isShielded)
        {
            hp-= dmg;
            hpSlider.SetHp(hp);

            if (hp == (maxHP/2))
            {
                isShielded = true;
                SpawnShieldTurrets();
            }
            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }

    }

    private void SpawnShieldTurrets()
    {
        foreach(GameObject child in childrens)
        {
            child.gameObject.SetActive(true);
        }
    }
    

    private void BigAttack()
    {

    }

    public void ShieldTurretDied()
    {
        numShieldTurrets--;
        if(numShieldTurrets <= 0)
        {
            isShielded = false;
        }
    }
}
