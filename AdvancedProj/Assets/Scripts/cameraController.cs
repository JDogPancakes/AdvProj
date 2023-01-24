using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] float cameraSpeed = 0.1f;

    private float rotation;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("q"))
        {
            rotation = 1;
        }
        else if (Input.GetKey("e"))
        {
            rotation = -1;
        }
        else
        {
            rotation = 0;
        }
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0, 0, rotation) * cameraSpeed * Time.deltaTime);
    }
}
