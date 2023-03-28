using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{

    [SerializeField]
    private Dungeon dungeon;

    public Transform roomPoint;

    private int randIndex;
    private GameObject currentRoom;
    private GameObject nextRoom;

    public GameObject startRoom;

    private int numRooms = 0;

    private void Awake()
    {
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

            randIndex = Random.Range(0, dungeon.tiles.Length);
            nextRoom = Instantiate(dungeon.tiles[randIndex], new Vector3(roomPoint.position.x, roomPoint.position.y, roomPoint.position.z), Quaternion.identity);
            numRooms++;
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

}
