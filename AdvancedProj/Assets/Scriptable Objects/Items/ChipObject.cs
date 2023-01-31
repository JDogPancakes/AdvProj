using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chip Object", menuName = "ScriptableObjects/Items/Chip")]
public class ChipObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Chip;
    }
}