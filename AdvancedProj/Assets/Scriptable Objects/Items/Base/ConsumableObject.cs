using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumableObject : ItemObject
{
    [HideInInspector] public int quantity;
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
        return true;

    }

    public override string Display()
    {
        return string.Format("{0}: {1}/{2}", name, quantity, maxQuantity);
    }
}

