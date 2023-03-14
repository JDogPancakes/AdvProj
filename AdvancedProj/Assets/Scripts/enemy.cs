using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    public Animator animator;
    private GameObject door;
    private Rigidbody2D rb;
    public int hp;
    public float moveSpeed = 6f;
    private int bitMask = ~((1 << 2) | (1 << 9));
    private NavMeshPath path;
    private Vector3 direction;


    private bool trackingPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        hp = 3;
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.isStopped = true;
        rb = GetComponent<Rigidbody2D>();
        path = new NavMeshPath();
        direction = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(agent.velocity.y) > Mathf.Abs(agent.velocity.x))
        {
            if (agent.velocity.y > 0)
            {
                Debug.Log("MoveDown");
                animator.SetBool("GoingRight", false);
                animator.SetBool("GoingLeft", false);
                animator.SetBool("GoingDown", true);
                animator.SetBool("GoingUp", false);
                animator.SetBool("Idle", false);
            }
            else
            {
                Debug.Log("MoveUp");
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
                Debug.Log("MoveRight");
                animator.SetBool("GoingRight", true);
                animator.SetBool("GoingLeft", false);
                animator.SetBool("GoingDown", false);
                animator.SetBool("GoingUp", false);
                animator.SetBool("Idle", false);
            }
            else
            {
                Debug.Log("MoveLeft");
                animator.SetBool("GoingRight", false);
                animator.SetBool("GoingLeft", true);
                animator.SetBool("GoingDown", false);
                animator.SetBool("GoingUp", false);
                animator.SetBool("Idle", false);

            }
        }
        else
        {
            animator.SetBool("GoingRight", false);
            animator.SetBool("GoingLeft", false);
            animator.SetBool("GoingDown", false);
            animator.SetBool("GoingUp", false);
            animator.SetBool("Idle", true);
        }
    }

    private void FixedUpdate()
    {
        Vector2 targetDir = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDir, 200f, bitMask);

        if (hit.collider != null && hit.collider.tag.Equals("Player"))
        {
            StopCoroutine(TrackPlayer());
            trackingPlayer = false;
            StartCoroutine(TrackPlayer());
        }

        int i = 1;
        while (i < path.corners.Length)
        {
            if (Vector3.Distance(transform.position, path.corners[i]) > 0.5f)
            {
                direction = path.corners[i] - transform.position;
                break;
            }
            i++;
        }

        rb.AddForce(direction * moveSpeed);
    }

    private IEnumerator TrackPlayer()
    {
        if (!trackingPlayer)
        {
            trackingPlayer = true;
            for (int i = 0; i < 20; i++)
            {
                agent.CalculatePath(target.position, path);
                yield return new WaitForSeconds(0.1f);
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
                door = GameObject.FindGameObjectWithTag("Door");
                door.GetComponentInChildren<Door>().EnemyDied(this.gameObject);
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
            rb.AddForce(collision.contacts[0].normal * 350f);
        }
    }
}
