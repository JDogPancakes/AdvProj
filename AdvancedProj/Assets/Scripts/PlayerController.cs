using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 6f;
    [SerializeField]
    private int hp = 5, maxHp = 5;
    public InventoryObject inventory;
    GameObject inventoryPanel;
    GameObject healthBar;
    GameObject[] healthChunks = new GameObject[5];
    private Rigidbody2D rb;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Camera cam;
    public Sprite[] spriteArray;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = transform.GetComponent<Rigidbody2D>();
        inventoryPanel = GameObject.Find("Player/PlayerUICanvas/InventoryPanel");
        healthBar = GameObject.Find("Player/PlayerUICanvas/HPBar");
        for (int i = 0; i < 5; i++)
        {
            healthChunks[i] = healthBar.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 targetVector = new Vector2(x, y);
        Move(targetVector.normalized);

        //dash
        if (Input.GetButtonDown("Chip"))
        {
            ActivateChip();
        }
        // Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        //Reloading
        if (Input.GetKeyDown(KeyCode.R) && inventory.weapon)
        {
            StartCoroutine(inventory.weapon.Reload());
        }
        //Open/close Inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
        //Consume Consumable
        if (Input.GetKeyDown(KeyCode.C))
        {
            inventory.consumable.Consume(this);

            if (inventory.consumable.quantity == 0)
            {
                inventory.consumable = null;
            }
        }
    }

    void Move(Vector2 targetDirection)
    {
        rb.velocity = targetDirection * movementSpeed;

        if (rb.velocity.y > 0)
        {
            spriteRenderer.sprite = spriteArray[0];
        }
        else if (rb.velocity.y < 0)
        {
            spriteRenderer.sprite = spriteArray[1];
        }
        else if (rb.velocity.x > 0)
        {
            spriteRenderer.sprite = spriteArray[3];
        }
        else if (rb.velocity.x < 0)
        {
            spriteRenderer.sprite = spriteArray[2];
        }
        if (inventory.armour)
        {
            rb.AddForce(targetDirection * movementSpeed * inventory.armour.moveSpeedModifier);
        }
        else
        {
            rb.AddForce(targetDirection * movementSpeed);
        }
    }

    void ActivateChip()
    {
        StartCoroutine(inventory.chip.Activate(gameObject));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Attempting to pick up item");
        Item item = other.GetComponent<Item>();
        if (item)
        {
            if (inventory.addItem(item.itemPrefab))
                Destroy(other.gameObject);

        }
    }

    private void OnApplicationQuit()
    {
        if (inventory) inventory.Clear();
    }

    // Shoot Method
    void Shoot()
    {
        if (inventory.weapon)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            if (inventory.weapon.ammo == 0)
            {
                StartCoroutine(inventory.weapon.Reload());
            }
            else
            {
                StartCoroutine(inventory.weapon.Attack(firePoint, angle));
            }
        }
    }

    void Damage(int damage)
    {
        while (damage > 0)
        {
            if (!inventory.armour || Random.Range(0, 100) > inventory.armour.blockChance)
            {
                hp--;
                healthChunks[hp].SetActive(false);
            }
            damage--;
            if (hp <= 0)
            {
                Die();
                break;
            }
        }

    }

    public void Heal(int heal)
    {
        while (heal > 0)
        {
            if (hp >= maxHp)
            {
                break;
            }
            healthChunks[hp].SetActive(true);
            hp++;
            heal--;
        }
    }

    public int getHP()
    {
        return hp;
    }
    public int getMaxHP()
    {
        return maxHp;
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
