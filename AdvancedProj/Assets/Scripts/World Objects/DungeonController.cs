using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class DungeonController : NetworkBehaviour
{

    [SerializeField]
    private Dungeon dungeon;

    [SerializeField]
    private List<Transform> roomDoors;

    [SerializeField]
    private Transform bossDoor;


    private int randIndex;

    private List<int> usedIndexs;

    private int numRooms = 0;

    override public void OnNetworkSpawn()
    {
        usedIndexs = new List<int>();
        GameObject startDoor = GameObject.Find("StartRoom/EntranceDoor");
        TeleportClientRpc(startDoor.transform.position.x, startDoor.transform.position.y + 1);

    }
    public IEnumerator MoveRoom()
    {
        yield return new WaitForSeconds(1);

        if (numRooms < dungeon.numRooms)
        {

            TryNextRoomServerRpc();

        }
        else
        {
            BossRoomServerRpc();
        }

    }

    [ServerRpc]
    private void BossRoomServerRpc()
    {
            Vector3 destination = bossDoor.transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.5f, 1.5f));
            TeleportClientRpc(destination.x, destination.y);
    }

    [ServerRpc]
    private void TryNextRoomServerRpc()
    {
        randIndex = Random.Range(0, dungeon.tiles.Length);
        if (!usedIndexs.Contains(randIndex))
        {
                Vector3 destination = roomDoors[randIndex].transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.5f, 1.5f));
                TeleportClientRpc(destination.x, destination.y);

            usedIndexs.Add(randIndex);
            numRooms++;
        }
        else
        {
            TryNextRoomServerRpc();
        }
    }

    [ClientRpc]
    private void TeleportClientRpc(float x, float y)
    {
        Debug.Log("Teleport");
        GameObject player = NetworkManager.Singleton.LocalClient.PlayerObject.gameObject;
        player.transform.position = new Vector3(x, y);

    }
}
