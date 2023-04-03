using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

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
            if(collision.gameObject.GetComponentInChildren<NetworkObject>().IsSpawned)collision.gameObject.BroadcastMessage("DamageServerRpc", 1);
        }
        Destroy(gameObject);
    }
}
