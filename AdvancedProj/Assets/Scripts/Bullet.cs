using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public int ricochetsRemaining = 0;
    public int damage = 1;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Hittable"))
        {
            if (collision.gameObject.GetComponentInChildren<NetworkObject>().IsSpawned) collision.gameObject.BroadcastMessage("DamageServerRpc", damage);
            Destroy(gameObject);
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
            } else
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
