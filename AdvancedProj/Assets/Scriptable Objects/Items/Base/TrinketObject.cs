using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TrinketObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Trinket;
    }
}