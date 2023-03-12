using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshObject : MonoBehaviour
{
    private bool updated = false;
    private void LateUpdate()
    {
        if (!updated)
        {
            updated = true;
            gameObject.GetComponent<NavMeshSurface>().BuildNavMeshAsync();
        }
    }
}
