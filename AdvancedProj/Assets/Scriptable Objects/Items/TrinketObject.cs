using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trinket Object", menuName = "ScriptableObjects/Items/Trinket")]
public class TrinketObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Trinket;
    }
}