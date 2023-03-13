using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;
    public LineRenderer lr;
    private bool trackingPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        lr = GameObject.Find("LineRenderer").GetComponent<LineRenderer>();
        target = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.path.corners.Length > 0) lr.SetPositions(agent.path.corners);
        Vector2 targetDir = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDir, 200f);

        if (hit.collider != null && hit.collider.tag.Equals("Player"))
        {
            StartCoroutine(TrackPlayer());
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
}
