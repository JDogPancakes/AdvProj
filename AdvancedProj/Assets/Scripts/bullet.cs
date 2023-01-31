using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float force = 20f;
    public Rigidbody2D rb;

    

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * force;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 mousePos = Input.mousePosition;

    }
}
