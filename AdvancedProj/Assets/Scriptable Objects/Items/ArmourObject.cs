using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armour Object", menuName = "ScriptableObjects/Items/Armour")]
public class ArmourObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Armour;
    }
}

