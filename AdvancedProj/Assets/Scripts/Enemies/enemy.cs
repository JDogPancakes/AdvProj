using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    public Animator animator;
    private Door door;
    public int hp;


    private bool trackingPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        hp = 3;
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 targetDir = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDir, 200f);

        if (hit.collider != null && hit.collider.tag.Equals("Player"))
        {
            StartCoroutine(TrackPlayer());
        }

        if (Mathf.Abs(agent.velocity.y) > Mathf.Abs(agent.velocity.x))
        {
            if (agent.velocity.y > 0)
            {
                animator.SetBool("GoingRight", false);
                animator.SetBool("GoingLeft", false);
                animator.SetBool("GoingDown", true);
                animator.SetBool("GoingUp", false);
                animator.SetBool("Idle", false);
            }
            else
            {
                animator.SetBool("GoingRight", false);
                animator.SetBool("GoingLeft", false);
                animator.SetBool("GoingDown", false);
                animator.SetBool("GoingUp", true);
                animator.SetBool("Idle", false);
            }
        }
        else if (Mathf.Abs(agent.velocity.y) < Mathf.Abs(agent.velocity.x))
        {
            if (agent.velocity.x > 0)
            {
                animator.SetBool("GoingRight", true);
                animator.SetBool("GoingLeft", false);
                animator.SetBool("GoingDown", false);
                animator.SetBool("GoingUp", false);
                animator.SetBool("Idle", false);
            }
            else
            {
                animator.SetBool("GoingRight", false);
                animator.SetBool("GoingLeft", true);
                animator.SetBool("GoingDown", false);
                animator.SetBool("GoingUp", false);
                animator.SetBool("Idle", false);

            }
        } else
        {
            animator.SetBool("GoingRight", false);
            animator.SetBool("GoingLeft", false);
            animator.SetBool("GoingDown", false);
            animator.SetBool("GoingUp", false);
            animator.SetBool("Idle", true);
        }


    }

    private IEnumerator TrackPlayer()
    {
        if (!trackingPlayer)
        {
            trackingPlayer = true;
            for (int i = 0; i < 4; i++)
            {
                agent.SetDestination(target.position);
                yield return new WaitForSeconds(0.5f);
            }
            trackingPlayer = false;
        }
    }
    public void Damage(int dmg)
    {
        hp--;
        if (hp <= 0)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            Destroy(gameObject);
            try
            {
                Debug.Log("Melee");
                door = transform.parent.GetComponentInChildren<Door>();
                door.EnemyDied(gameObject);
            }
            catch (System.NullReferenceException)
            {
                Debug.Log("Door Not Found");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.BroadcastMessage("Damage", 1);

        }
    }
}
