using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject lockedDoor;

    private Animator animator;

    private bool doorisLocked = true;

    private void Awake()
    {
        animator= GetComponent<Animator>(); 

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(doorisLocked == false)
        {
            animator.SetBool("openDoor", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (lockedDoor.activeSelf == false)
        {
            animator.SetBool("openDoor", false);
        }
    }

    private void UnlockDoor()
    {
        lockedDoor.SetActive(false);
        doorisLocked= false;    
    }

    public void LockDoor()
    {
        doorisLocked = true;
        lockedDoor.SetActive(true);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            UnlockDoor();
        }
    }
}
