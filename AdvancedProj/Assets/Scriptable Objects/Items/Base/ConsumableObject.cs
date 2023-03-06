using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumableObject : ItemObject
{
    public int quantity;
    public int maxQuantity;
    public void Awake()
    {
        type = ItemType.Consumable;
    }

    abstract public void Consume(PlayerController target);
    public bool add()
    {
        if (quantity >= maxQuantity) return false;
        quantity++;
        Debug.Log(quantity);
        return true;

    }
}

