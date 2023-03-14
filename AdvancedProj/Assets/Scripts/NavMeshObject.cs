using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshObject : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(UpdateNavMesh());
    }

    private IEnumerator UpdateNavMesh()
    {
        yield return new WaitForEndOfFrame();
        gameObject.GetComponent<NavMeshSurface>().BuildNavMeshAsync();
    }
}
