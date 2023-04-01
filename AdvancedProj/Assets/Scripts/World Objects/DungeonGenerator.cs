using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{

    [SerializeField]
    private Dungeon dungeon;

    [SerializeField]
    private List<Transform> roomDoors;

    [SerializeField]
    private Transform bossDoor;


    private int randIndex;
    private GameObject player;

    private List<int> usedIndexs;

    private int numRooms = 0;

    private void Awake()
    {
        usedIndexs = new List<int>();
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
    public void MoveRoom()
    {
        

        if (numRooms < dungeon.numRooms)
        {

            TryNextRoom();
            
        }
        else
        {
            BossRoom();
        }

    }


    private void BossRoom()
    {

        player.transform.position = bossDoor.transform.position + new Vector3(0, 1);
    }

    private void TryNextRoom()
    {
        randIndex = Random.Range(0, dungeon.tiles.Length);
        if (!usedIndexs.Contains(randIndex))
        {
            player.transform.position = roomDoors[randIndex].transform.position + new Vector3(0, 1);
            usedIndexs.Add(randIndex);
            numRooms++;
        }
        else
        {
            TryNextRoom();
        }

    }
}
