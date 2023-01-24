using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem_ControlSystem : MonoBehaviour
{
    [SerializeField]
    List<Player_Interact> playerList;
    [SerializeField]
    List<Interactable> interactableList;

    // Start is called before the first frame update
    void Awake()
    {
        playerList = new List<Player_Interact>();
        interactableList = new List<Interactable>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ScanningInteractable();
    }

    private void ScanningInteractable()
    {
        foreach (Player_Interact player in playerList)
        {
            foreach (Interactable interactable in interactableList)
            {
                if (player.ContainItemReference(interactable)) 
                {
                    continue;
                }
                else if (player.InRange(interactable.transform))
                {
                    player.AddItem(interactable);
                }
                
            }
        }
    }

    /// <summary>
    /// When player spawn, Call this function to add into global reference
    /// </summary>
    internal void AddPlayerToSystem(Player_Interact player_Interact) {
        playerList.Add(player_Interact);
    }

    /// <summary>
    /// When interactable spawn, Call this function to add into global reference
    /// </summary>
    internal void AddInteractableToSystem(Interactable interactable)
    {
        interactableList.Add(interactable);
    }


}
