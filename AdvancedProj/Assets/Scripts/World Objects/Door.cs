using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

public class Door : NetworkBehaviour
{
    public GameObject lockedDoor;
    private DungeonController dc;

    private Animator animator;

    public bool doorIsLocked = true;

    private List<GameObject> enemies;
    private int waitingPlayers = 0;
    public TextMeshProUGUI playerCountText;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemies = new List<GameObject>();
        dc = FindObjectOfType<DungeonController>();
        GetEnemiesInRoom();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            OpenDoorClientRpc();
        }
    }

    public void EnemyDied(GameObject enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
            Debug.Log(enemies.Count);
        }
        if (enemies.Count == 0)
        {
            OpenDoorClientRpc();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsServer && collision.CompareTag("Player"))
        {
            PlayerEnterServerRpc();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsServer && collision.CompareTag("Player"))
        {
            PlayerExitServerRpc();
        }
    }


    [ServerRpc]
    private void PlayerEnterServerRpc()
    {
        waitingPlayers++;
        int totalPlayers = NetworkManager.ConnectedClients.Count;
        UpdateTextClientRpc(waitingPlayers, totalPlayers);

        //open door if any players are near it
        if (!doorIsLocked && waitingPlayers == totalPlayers)
        {
                dc.StartCoroutine(dc.MoveRoom());
        }
    }

    [ServerRpc]
    private void PlayerExitServerRpc()
    {
        int totalPlayers = NetworkManager.ConnectedClients.Count;
        waitingPlayers--;
        UpdateTextClientRpc(waitingPlayers, totalPlayers);
    }

    [ClientRpc]
    private void UpdateTextClientRpc(int waiting, int required)
    {
        if (waiting > 0)
        {
            playerCountText.text = string.Format("{0} / {1}", waiting, required);
            animator.SetBool("openDoor", true);
        }
        else
        {
            playerCountText.text = "";
            animator.SetBool("openDoor", false);
        }
    }


    [ClientRpc]
    private void OpenDoorClientRpc()
    {
        lockedDoor.SetActive(false);
        doorIsLocked = false;
    }
    [ClientRpc]
    public void LockDoorClientRpc()
    {
        doorIsLocked = true;
        lockedDoor.SetActive(true);
    }

    private void GetEnemiesInRoom()
    {
        foreach (Transform t in transform.parent.parent.GetComponentsInChildren<Transform>())
        {
            if (t.CompareTag("Enemy"))
            {
                enemies.Add(t.gameObject);
            }
        }
    }

}
