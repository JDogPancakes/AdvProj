using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public float movementSpeed = 6f;
    [SerializeField]
    private int hp = 5, maxHp = 5;
    public InventoryManager inventory;
    public GameObject bulletPrefab;
    [SerializeField] private GameObject mainInventoryGroup;
    [SerializeField] private Canvas UICanvas;
    [SerializeField] private GameObject[] healthChunks = new GameObject[5];
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform firePoint;
    public Camera cam;
    public Sprite[] spriteArray;
    public Animator playerAnimator;
    public SpriteRenderer spriteRenderer;
    public AudioSource walking;
    private LineRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        if (!IsLocalPlayer) return;
        cam.gameObject.SetActive(true);
        cam.GetComponent<AudioListener>().gameObject.SetActive(true);
        UICanvas.gameObject.SetActive(true);
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
    }



    // Update is called once per frame
    void Update()
    {
        if (!IsLocalPlayer) return;
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
        if (Input.GetButton("Fire1") && !mainInventoryGroup.activeInHierarchy)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 lookDir = mousePos - rb.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            ShootServerRpc(angle);
        }
        //Reloading
        if (inventory.hasWeapon())
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(inventory.getWeapon().Reload(inventory.getWeaponSlot()));
            }
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

        if (rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            walking.Play();
            playerAnimator.ResetTrigger("Moving");
        }
        else
        {
            playerAnimator.SetTrigger("Moving");
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
                other.gameObject.GetComponent<NetworkObject>().Despawn();
        }
    }

    // Shoot Method
    [ServerRpc(RequireOwnership = false)]
    void ShootServerRpc(float angle)
    {
        if (inventory.hasWeapon())
        {
            if (inventory.getWeapon().ammo == 0)
            {
                StartCoroutine(inventory.getWeapon().Reload(inventory.getWeaponSlot()));
            }
            else
            {
                StartCoroutine(inventory.getWeapon().Attack(this, angle));
            }
        }
    }

    [ClientRpc]
    public void SpawnBulletClientRpc(Quaternion rotation, int dmg = 1, int ricochets = 0, int pierce = 0, float speed = 15)
    {
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, rotation).GetComponent<Bullet>();
        GetComponents<AudioSource>()[1].Play();
        bullet.damage = dmg;
        bullet.ricochetsRemaining = ricochets;
        bullet.pierce = pierce;
        bullet.SetSpeed(speed);
        if (IsOwner) inventory.getWeaponSlot().UpdateItem();
    }

    [ServerRpc]
    void DamageServerRpc(int damage)
    {
        Debug.Log("Player Hit ");
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

    public int GetHP()
    {
        return hp;
    }
    public int GetMaxHP()
    {
        return maxHp;
    }
    void Die()
    {
        GetComponent<NetworkObject>().Despawn();
    }
}
