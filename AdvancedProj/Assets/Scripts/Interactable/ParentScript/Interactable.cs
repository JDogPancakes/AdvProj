using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Communicate Value to Player and System
    internal bool can_interact = true;
    internal bool actived = false;


    [SerializeField]
    private InteractableItem_ControlSystem system;
    [SerializeField] 
    private GameObject display_text;
    [SerializeField]
    private Animator animator;


    internal bool currently_show_text = false;

    private void Awake()
    {
        if (!display_text)
        {
            display_text = transform.Find("OnTopMessageCanvas").gameObject;
            display_text.SetActive(false);
        }
        if (!animator)
        {
            animator = gameObject.GetComponent<Animator>();
        }
    }
    private void Start()
    {
        system = GameObject.FindGameObjectWithTag("InteractableSystem").
            GetComponent<InteractableItem_ControlSystem>();
        system.AddInteractableToSystem(this);
    }

    private void FixedUpdate()
    {
        if (currently_show_text && !display_text.activeSelf)
        {
            ShowText();
        }

        if (!currently_show_text && display_text.activeSelf)
        {
            HideText();
        }
    }

    /// <summary>
    /// Show the On Top Message
    /// </summary>
    internal void ShowText()
    {
        Debug.Log("You can interact this with 'E' Key.");
        display_text.SetActive(true);
    }

    /// <summary>
    /// Hide the On Top Message
    /// </summary>
    internal void HideText()
    {
        display_text.SetActive(false);
    }

    /// <summary>
    /// When Someone Trying to Interact with this Interactable.
    /// </summary>
    internal void Try_Interact() {

        if (!actived)
        {
            Debug.Log("Trying to Interact with this Object.");
            can_interact = false;
            actived = true;
            animator.SetTrigger("Active");
            animator.SetBool("Actived",true);

        }
    }

    /// <summary>
    /// The main interact function, what will happen if you interact with it
    /// </summary>
    internal void Interact() {
        Debug.Log("This interavtable should do something now.");
    }

}
