using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="T-Shirt", menuName = "ScriptableObjects/Armour/T-Shirt")]
public class TShirtObject : ArmourObject
{
    new public void Awake()
    {
        base.Awake();
        blockChance = 20f;
        moveSpeedModifier = 1.2f;
    }
}
