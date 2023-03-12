using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject lockedDoor;

    private Animator animator;

    private bool doorisLocked = true;
    private List<GameObject> enemies;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        
    }

    public void EnemyDied(GameObject enemy)
    {
        if (enemies.Contains(enemy)){
            enemies.Remove(enemy);
        }
        if(enemies.Count == 0)
        {
            UnlockDoor();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!doorisLocked)
        {
            animator.SetBool("openDoor", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!doorisLocked)
        {
            animator.SetBool("openDoor", false);
        }
    }

    private void UnlockDoor()
    {
        lockedDoor.SetActive(false);
        doorisLocked= false;    
    }

    public void LockDoor()
    {
        doorisLocked = true;
        lockedDoor.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            UnlockDoor();
        }
    }
}
