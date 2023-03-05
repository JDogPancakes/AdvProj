using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject entranceDoor;
    public GameObject lockedDoor;

    private void Awake()
    {
        
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.position = entranceDoor.transform.position + new Vector3(0, 1) ;
            lockedDoor.GetComponent<Door>().LockDoor();
            lockedDoor.GetComponent<Animator>().SetBool("opnDoor", false);
        }
    }
}
