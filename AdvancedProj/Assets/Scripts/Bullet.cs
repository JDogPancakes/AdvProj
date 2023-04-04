using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public int ricochetsRemaining = 0;
    [HideInInspector] public int damage = 1;
    [HideInInspector] public int pierce = 0;
    private float speed = 15f;

    private Rigidbody2D rb;
    private LineRenderer lr;

    // Start is called before the first frame update
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        SetSpeed(15f);
        lr = GetComponent<LineRenderer>();
        if (lr) lr.SetPositions(new Vector3[] { transform.position, transform.position });
    }

    public void SetSpeed(float speed)
    {
        rb.velocity = transform.up * speed;
        this.speed = speed;
    }

    private void Update()
    {
        if (lr)
        {
            lr.SetPosition(1, transform.position);
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Hittable"))
        {
            Debug.Log("Hit Hittable");
            collision.gameObject.BroadcastMessage("DamageServerRpc", damage);
            if (pierce > 0) pierce--;
            else Destroy(gameObject);
        }
        else if (ricochetsRemaining > 0)
        {
            Debug.Log("Ricochet");
            ricochetsRemaining--;
            Vector3 normal = collision.GetContact(0).normal;
            Debug.Log("Normal: " + normal);
            Vector3 direction = transform.rotation.eulerAngles;
            Debug.Log("Direction: " + direction);

            if (Mathf.Abs(normal.x) == 1)
            {
                direction.z = (180 - direction.z) + 180;
            }
            else if (Mathf.Abs(normal.y) == 1)
            {
                direction.z = (90 - direction.z) + 90;
            }
            else
            {
                direction = Vector3.Reflect(direction.normalized, normal);
            }

            Quaternion newRotation = new Quaternion();
            newRotation.eulerAngles = direction;
            transform.rotation = newRotation;
            rb.velocity = transform.up * speed;
        }
        else Destroy(gameObject);
    }
}
