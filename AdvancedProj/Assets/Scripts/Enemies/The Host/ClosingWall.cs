using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ClosingWall : NetworkBehaviour
{
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject wall;
    private int numPlayers;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsServer && collision.CompareTag("Player"))
        {
            numPlayers++;
            if (numPlayers == NetworkManager.ConnectedClients.Count)
            {
                SpawnWallClientRpc();
                ActivateBoss();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsServer && collision.CompareTag("Player"))
        {
            numPlayers--;
        }
    }
    private void ActivateBoss()
    {
        boss.GetComponentInChildren<BossController>().canAttack = true;
        GetComponent<NetworkObject>().Despawn();
    }

    [ClientRpc]
    private void SpawnWallClientRpc()
    {
        Debug.Log("Spawning wall");
        wall.SetActive(true);
    }
}
