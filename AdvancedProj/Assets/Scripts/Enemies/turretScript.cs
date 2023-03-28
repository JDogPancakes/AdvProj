using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class turretScript : MonoBehaviour
{
    private Transform target;
    public GameObject bulletPrefab;
    private GameObject door;
    public bool canAttack = true, reloading = false;
    public Transform firepoint;

    public int hp;
    public int currentAmmo = 2, maxAmmo = 2;
    public float reloadDelay = 2f;
    public float attackDelay;

    private RaycastHit2D hit;
    private LayerMask lm;
    private Animator animator;

    // Start is called before the first frame update
    void Awake()
    {

        animator = gameObject.GetComponent<Animator>();
        attackDelay = 0.3f;
        hp = 3;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        Physics2D.queriesStartInColliders = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            return;
        }




    }

    private void FixedUpdate()
    {
        Vector2 targetDir = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
        hit = Physics2D.Raycast(transform.position, targetDir, 200f);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Player")
                Shoot(targetDir);
        }
    }

    void Shoot(Vector2 shootDir)
    {

        float angle = Mathf.Atan2(shootDir.y, shootDir.x) * Mathf.Rad2Deg - 90f;
        transform.eulerAngles = new Vector3(0, 0, angle);
        StartCoroutine(Attack(firepoint, angle));
    }

    public IEnumerator Attack(Transform firepoint, float angle)
    {
        //if there's ammo & the last attack was long enough ago
        if (currentAmmo > 0 && !reloading)
        {
            if (canAttack)
            {
                animator.SetTrigger("Shooting");
                canAttack = false;
                //calculate angle & fire
                for (currentAmmo = 3; currentAmmo > 0;currentAmmo--)
                {
                    Quaternion qt = new Quaternion();
                    qt.eulerAngles = new Vector3(0, 0, angle);
                    GameObject currentBullet = Instantiate(bulletPrefab, firepoint.position, qt);
                    currentBullet.layer = 10;
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

    public void Damage(int dmg)
    {
        hp--;
        if (hp <= 0)
        {
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject.transform.parent.gameObject);
            try
            {
                door = GameObject.FindGameObjectWithTag("Door");
                door.GetComponentInChildren<Door>().EnemyDied(this.gameObject.transform.parent.gameObject);
            } catch (System.NullReferenceException) 
            {
                Debug.Log("Door Not Found");
            }
        }
    }
}
