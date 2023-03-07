using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArmourObject : ItemObject
{
    public float blockChance;
    public float moveSpeedModifier;

    public void Awake()
    {
        type = ItemType.Armour;
    }
}

