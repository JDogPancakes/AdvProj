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

    private int numRooms = 0;


    public void GenerateRoom()
    {
        if (currentRoom != null)
        {
            Destroy(currentRoom);
        }

        if (numRooms <= dungeon.tiles.Length)
        {

            randIndex = Random.Range(0, dungeon.tiles.Length);
            currentRoom = Instantiate(dungeon.tiles[randIndex], new Vector3(roomPoint.position.x, roomPoint.position.y, roomPoint.position.z), Quaternion.identity);
            numRooms++;
        }
        else
        {
            GenerateBossRoom();
        }
        
       
    }

    private void GenerateNextRoom()
    {

    }

    private void GenerateBossRoom()
    {

    }
   
}
