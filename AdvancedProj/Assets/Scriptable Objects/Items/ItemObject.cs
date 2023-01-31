using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Weapon,
    Consumable,
    Armour,
    Trinket,
    Chip
}
public abstract class ItemObject : ScriptableObject
{
    public Sprite sprite;
    public ItemType type;
    [TextArea(10, 20)]
    public string description;
}
