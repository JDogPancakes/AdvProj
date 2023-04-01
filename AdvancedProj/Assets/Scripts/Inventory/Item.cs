using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(NetworkObject))]
public class Item : NetworkBehaviour
{
    public ItemObject[] possibleItems;
    private ItemObject _itemPrefab;
    public ItemObject itemPrefab
    {
        get
        {
            return _itemPrefab;
        }
    }

    public override void OnNetworkSpawn()
    {
        //choose item from list
        if (possibleItems.Length > 1)
        {
            int selectedItem = Random.Range(0, possibleItems.Length);
            _itemPrefab = possibleItems[selectedItem];
        }
        else _itemPrefab = possibleItems[0];
        GetComponentInChildren<SpriteRenderer>().sprite = _itemPrefab.sprite;

        //generate polygon collider
        PolygonCollider2D col = gameObject.AddComponent<PolygonCollider2D>();
        col.isTrigger = true;
        col.pathCount = 0;
        col.pathCount = _itemPrefab.sprite.GetPhysicsShapeCount();

        List<Vector2> path = new List<Vector2>();
        for (int i = 0; i < col.pathCount; i++)
        {
            path.Clear();
            _itemPrefab.sprite.GetPhysicsShape(i, path);
            col.SetPath(i, path.ToArray());
        }
    }
}
