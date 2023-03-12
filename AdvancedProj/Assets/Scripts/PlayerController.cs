using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 6f;
    [SerializeField]
    private int hp = 5, maxHp = 5;
    public InventoryManager inventory;
    GameObject mainInventoryGroup;
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
        healthBar = GameObject.Find("Player/PlayerUICanvas/HPBar");
        mainInventoryGroup = GameObject.Find("Player/PlayerUICanvas/MainInventoryGroup");
        for (int i = 0; i < 5; i++)
        {
            healthChunks[i] = healthBar.transform.GetChild(i).gameObject;
        }
        mainInventoryGroup.gameObject.SetActive(false);
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
        if (Input.GetButtonDown("Fire1") && !mainInventoryGroup.gameObject.activeInHierarchy)
        {
            Shoot();
        }
        //Reloading
        if (inventory.hasWeapon())
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(inventory.getWeapon().Reload());
            }
            else if (inventory.getWeapon().reloading)
            inventory.getWeaponSlot().UpdateItem();
        }
        //Open/close Inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            mainInventoryGroup.SetActive(!mainInventoryGroup.activeInHierarchy);
        }
        //Consume Consumable
        if (Input.GetKeyDown(KeyCode.C))
        {
            inventory.getConsumable().Consume(this);
            if (inventory.getConsumable().quantity <= 0) inventory.RemoveItem(4);
            else inventory.getConsumableSlot().UpdateItem();
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
        if (inventory.hasArmour())
        {
            rb.AddForce(targetDirection * movementSpeed * inventory.getArmour().moveSpeedModifier);
        }
        else
        {
            rb.AddForce(targetDirection * movementSpeed);
        }
    }

    void ActivateChip()
    {
        StartCoroutine(inventory.getChip().Activate(gameObject));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Attempting to pick up item");
        Item item = other.GetComponent<Item>();
        if (item)
        {
            if (inventory.AddItem(item.itemPrefab))
                Destroy(other.gameObject);

        }
    }

    /*    private void OnApplicationQuit()
        {
            if (inventory) inventory.Clear();
        }*/

    // Shoot Method
    void Shoot()
    {
        if (inventory.hasWeapon())
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            if (inventory.getWeapon().ammo == 0)
            {
                StartCoroutine(inventory.getWeapon().Reload());
            }
            else
            {
                StartCoroutine(inventory.getWeapon().Attack(firePoint, angle));
                inventory.getWeaponSlot().UpdateItem();
            }
        }
    }

    void Damage(int damage)
    {
        while (damage > 0)
        {
            if (!inventory.hasArmour() || Random.Range(0, 100) > inventory.getArmour().blockChance)
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
                return;
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
