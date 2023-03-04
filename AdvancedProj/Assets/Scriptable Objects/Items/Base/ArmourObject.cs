using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmourObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Armour;
    }
}

