using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemObject itemPrefab;
    
    void Awake()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = itemPrefab.sprite;
    }
}
