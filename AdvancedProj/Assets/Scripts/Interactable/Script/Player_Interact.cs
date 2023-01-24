using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interact with Key [ E ]
/// This Script for Player to interact with interactable objects
///     Such as Loot chest
/// </summary>
public class Player_Interact : MonoBehaviour
{
    //All the interactable refrence player holding
    private Dictionary<Interactable, Boolean> interactableList;
    
    [SerializeField]
    private InteractableItem_ControlSystem system;
    
    //How far can the player interact with an interactable
    [SerializeField]
    float interact_Range = 1.5f;

    //Reference of the last encontered interactable
    private Interactable last_interacted;

    //Warning: This is locker value, control specific function to prevent
    // looping when the collection is changing, edit if only you know what this for.
    private int lockerControlInteger = 1;

    private void Awake()
    {
        interactableList = new Dictionary<Interactable, Boolean>();
    }

    private void Start()
    {
        system = GameObject.FindGameObjectWithTag("InteractableSystem").
            GetComponent<InteractableItem_ControlSystem>();

        system.AddPlayerToSystem(this);
    }

    private void Update()
    {
        ScanInteractables();
        Interact();
    }

    private void FixedUpdate()
    {
        RemoveByState();
        ShowTextInteractable();
    }

    /// <summary>
    /// Enable the on top text of the interactable.
    /// </summary>
    private void ShowTextInteractable()
    {
        if (lockerControlInteger == 0)
        {
            return;
        }

        foreach (KeyValuePair<Interactable,Boolean> interactable in interactableList)
        {
            interactable.Key.currently_show_text = true;
        }
    }

    /// <summary>
    /// Update states while scanning thought all the reference interables.
    /// </summary>
    private void ScanInteractables()
    {
        if (lockerControlInteger == 0)
        {
            return;
        }

        try { 
            foreach (KeyValuePair<Interactable, Boolean> interactable in interactableList)
            {
                //Add remove state for Out Of Range
                if (!InRange(interactable.Key.transform))
                {
                    interactable.Key.currently_show_text = false;
                    interactableList[interactable.Key] = false;
                }
                //Add remove state if already actived
                if (interactable.Key.actived)
                {
                    interactable.Key.currently_show_text = false;
                    interactableList[interactable.Key] = false;
                }
            }
        }
        catch (InvalidOperationException e)
        {

            Debug.LogWarning("Catched Invalid Operation Exception in Player_Interaction script, Be aware the function is been control by locker value.");
            Debug.Log("Ff there is better way to solve this run time changing Dictionary problem, you can disable the locker value to start with." + e);
        }

    }

    /// <summary>
    /// Removing all reference with a remove state (FALSE Value)
    /// </summary>
    private void RemoveByState() 
    {
        if (lockerControlInteger == 0)
        {
            return;
        }

        lockerControlInteger--;

        try
        {
            foreach (KeyValuePair<Interactable, Boolean> interactable in interactableList)
            {
                if (!interactable.Value)
                {
                    interactableList.Remove(interactable.Key);
                }
            }
        }
        catch (InvalidOperationException e)
        {

            Debug.LogWarning("Catched Invalid Operation Exception in Player_Interaction script, Be aware the function is been control by locker value, if there is better way to solve this run time changing Dictionary problem, you can disable the locker value.");
            Debug.LogWarning(e);
        }
        

        lockerControlInteger++;
    }

    /// <summary>
    /// Give the distance between target transform to the gameObject
    /// </summary>
    /// <param name="target_transform"> The target</param>
    /// <returns></returns>
    private float DistanceOf(Transform target_transform)
    {
        return Vector2.Distance(transform.position, target_transform.position);
    }


    /// <summary>
    /// If an interactable is in the enconter range of the player
    /// </summary>
    /// <param name="target_transform"></param>
    /// <returns></returns>
    internal bool InRange(Transform target_transform) 
    {
        return DistanceOf(target_transform) <= interact_Range;
    }

    /// <summary>
    /// Add new interactable into the player reference.
    /// </summary>
    /// <param name="interactable">The new interactable script you want to add into the reference. </param>
    internal void AddItem(Interactable interactable) 
    {
        interactableList.Add(interactable,true);
        last_interacted = interactable;
    }

    /// <summary>
    /// If the player already have one of the reference of target in the Dictionary.
    /// </summary>
    /// <param name="interactable"> The target interactiable that need to check </param>
    /// <returns></returns>
    internal bool ContainItemReference(Interactable interactable)
    {
        return interactableList.ContainsKey(interactable);
    }


    /// <summary>
    /// By Key "E", Trying to interact with the closest interactable
    /// </summary>
    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactableList.Count > 0)
            {
                GetTClosestInteractable().Try_Interact();
            }
        }
    }

    /// <summary>
    /// Search the closest interactable in the Dictionary
    /// </summary>
    /// <returns></returns>
    private Interactable GetTClosestInteractable()
    {
        if (lockerControlInteger == 0)
        {
            return last_interacted;
        }

        lockerControlInteger--;

        Interactable theClosest = last_interacted;

        foreach (KeyValuePair<Interactable, Boolean> item in interactableList)
        {
            if (DistanceOf(item.Key.transform) <= DistanceOf(theClosest.transform))
            {
                theClosest = item.Key;
            }
        }

        lockerControlInteger++;
        
        return theClosest;
    }
}
