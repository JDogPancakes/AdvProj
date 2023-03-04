using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{

    [SerializeField]
    private Dungeon dungeon;

    public Transform roomPoint;

    private int randIndex;
    private GameObject[] createdRooms;

    private int showRoomId = 0;



    void Awake()
    {
        createdRooms = new GameObject[dungeon.numRooms];
        GenerateDungeon();
        
    }

    private void GenerateDungeon()
    {

        for(int x = 0; x <= dungeon.numRooms - 1; x++)
        {
            roomPoint.position += new Vector3(0, 12f,0 );

            randIndex = Random.Range(0, dungeon.tiles.Length);

            createdRooms[x] = Instantiate(dungeon.tiles[randIndex], new Vector3(roomPoint.position.x, roomPoint.position.y,roomPoint.position.z), Quaternion.identity);

           
        }

        GenerateBossRoom();
        HideRooms(createdRooms);
    }

    private void GenerateBossRoom()
    {

    }

    private void HideRooms(GameObject[] rooms)
    {
        foreach(GameObject room in rooms)
        {
            room.SetActive(false);
        }
    }

    private void ShowNextRoom()
    {
        
    }
}
