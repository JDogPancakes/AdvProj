using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class Door : NetworkBehaviour
{
    public GameObject lockedDoor;

    private Animator animator;

    private bool doorisLocked = true;

    private List<GameObject> enemies;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemies= new List<GameObject>();
        GetEnemiesInRoom();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnlockDoorServerRpc();
        }
    }

    public void EnemyDied(GameObject enemy)
    {

        if (enemies.Contains(enemy)){
            enemies.Remove(enemy);
            Debug.Log(enemies.Count);
        }
        if(enemies.Count == 0)
        {
            UnlockDoorServerRpc();
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

    [ServerRpc]
    private void UnlockDoorServerRpc()
    {
        lockedDoor.SetActive(false);
        doorisLocked = false;    
    }
    [ServerRpc]
    public void LockDoorServerRpc()
    {
        doorisLocked = true;
        lockedDoor.SetActive(true);
    }

    private void GetEnemiesInRoom()
    {

        foreach(Transform t in transform.parent.parent.GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag("Enemy"))
            {
                enemies.Add(t.gameObject);
            }

        }
    }

}
