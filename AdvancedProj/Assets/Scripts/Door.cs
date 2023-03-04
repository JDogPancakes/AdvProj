using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject lockedDoor;

    private Animator animator;

    private void Awake()
    {
        animator= GetComponent<Animator>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(lockedDoor.activeSelf == false)
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
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            UnlockDoor();
        }
    }
}
