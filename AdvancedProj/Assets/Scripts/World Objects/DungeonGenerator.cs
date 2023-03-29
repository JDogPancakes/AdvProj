using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{

    [SerializeField]
    private Dungeon dungeon;

    public Transform roomPoint;
    public GameObject startRoom;

    private int randIndex;
    private GameObject currentRoom;
    private GameObject nextRoom;

    private List<int> usedIndexs;

    private bool canCreateRoom = true;

    private int numRooms = 0;

    private void Awake()
    {
        usedIndexs = new List<int>();
        currentRoom = startRoom;
    }
    public void GenerateRoom()
    {
        if (currentRoom != null)
        {
            Destroy(currentRoom);
        }

        if (numRooms < dungeon.numRooms)
        {

            TryMakeRoom();
            
        }
        else
        {
            GenerateBossRoom();
        }

        currentRoom = nextRoom;
        currentRoom.transform.SetAsFirstSibling();

    }


    private void GenerateBossRoom()
    {
        Destroy(currentRoom);

        nextRoom = Instantiate(dungeon.bossTile, new Vector3(roomPoint.position.x, roomPoint.position.y, roomPoint.position.z), Quaternion.identity);
    }

    private void TryMakeRoom()
    {
        randIndex = Random.Range(0, dungeon.tiles.Length);
        if (!usedIndexs.Contains(randIndex))
        {
            nextRoom = Instantiate(dungeon.tiles[randIndex], new Vector3(roomPoint.position.x, roomPoint.position.y, roomPoint.position.z), Quaternion.identity);
            usedIndexs.Add(randIndex);
            numRooms++;
        }
        else
        {
            TryMakeRoom();
        }

    }
}
