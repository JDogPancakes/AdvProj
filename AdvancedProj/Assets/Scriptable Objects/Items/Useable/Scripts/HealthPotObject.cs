using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health Pot", menuName = "ScriptableObjects/Consumable/Health Pot")]
public class HealthPotObject : ConsumableObject
{

    new public void Awake()
    {
        base.Awake();
        maxQuantity = 2;
        quantity = 0;
    }

    public override void Consume(PlayerController target)
    {
        if(target.getHP() < target.getMaxHP())
        {
            quantity--;
            target.Heal(2);
        }
    }
}
