using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject entranceDoor;
    public GameObject lockedDoor;
    private GameObject dGen;


    private void Awake()
    {
        dGen = GameObject.FindGameObjectWithTag("GameController");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            dGen.GetComponent<DungeonGenerator>().MoveRoom();
        }
    }
}
