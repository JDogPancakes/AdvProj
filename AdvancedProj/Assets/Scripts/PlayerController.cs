using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float movementSpeed = 20f;
    public float dashStrength = 2000f;
    public InventoryObject inventory;
    public GameObject inventoryCanvas;

    private Rigidbody2D rb;

    public Transform firePoint;
    public GameObject bulletPrefab;

    private Quaternion qt;

    Vector2 mousePos;
    public Camera cam;

    // Start is called before the first frame update
    void Awake()
    {
        rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 targetVelocity = new Vector2(x, y);

        Move(targetVelocity);

        if (Input.GetButtonDown("Dash"))
        {
            Dash();
        }


        // Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

        //Open/close Inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryCanvas.SetActive(!inventoryCanvas.activeInHierarchy);
        }

    }

    void Move(Vector2 targetVelocity)
    {
        //rb.position += ((targetVelocity * movementSpeed) * Time.deltaTime);
        rb.AddForce(targetVelocity * movementSpeed);
    }


    void Dash()
    {
        rb.AddForce(rb.velocity.normalized * dashStrength);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Attempting to pick up item");
        Item item = other.GetComponent<Item>();
        if (item)
        {
            if (inventory.addItem(item.item))
                Destroy(other.gameObject);

        }
    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
    }

    // Shoot Method
    void Shoot()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

        qt.eulerAngles = new Vector3(0, 0, angle);

        Instantiate(bulletPrefab, firePoint.position, qt);
}
}
