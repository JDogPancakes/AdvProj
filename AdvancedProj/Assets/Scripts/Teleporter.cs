using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject entranceDoor;
    public GameObject lockedDoor;
    private GameObject dGen;
    private GameObject player;


    private void Awake()
    {
        dGen = GameObject.FindGameObjectWithTag("GameController");
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = entranceDoor.transform.position + new Vector3(0, 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            dGen.GetComponent<DungeonGenerator>().GenerateRoom();
        }
    }
}
