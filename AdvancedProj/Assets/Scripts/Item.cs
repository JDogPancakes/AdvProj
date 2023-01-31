using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemObject item;
    
    void Awake()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.sprite;
    }
}
