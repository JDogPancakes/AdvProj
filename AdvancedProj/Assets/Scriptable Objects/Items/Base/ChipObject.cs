using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChipObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Chip;
    }
}