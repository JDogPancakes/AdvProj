using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : NetworkBehaviour
{
    private NavMeshAgent agent;
    public Animator animator;
    private Door door;
    public int hp;


    private bool trackingPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        hp = 3;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!IsServer) return;
        //get list of targets
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        //find nearest target
        float minDistance = Mathf.Infinity;
        GameObject closestTarget = null;
        foreach(GameObject target in targets){
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if(distance < minDistance)
            {
                minDistance = distance;
                closestTarget = target;
            }
        }
        Vector2 targetDir = (closestTarget.transform.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDir, 200f);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            StartCoroutine(TrackPlayer(closestTarget.transform));
        }

        //HandleAnimation();
    }

    private void HandleAnimation()
    {
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

    private IEnumerator TrackPlayer(Transform target)
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
    [ServerRpc]
    public void DamageServerRpc(int dmg)
    {
        hp-= dmg;
        if (hp <= 0)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<NetworkObject>().Despawn();
            try
            {
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
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.BroadcastMessage("DamageServerRpc", 1);

        }
    }
}
