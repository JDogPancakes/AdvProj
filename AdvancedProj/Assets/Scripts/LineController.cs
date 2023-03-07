using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [SerializeField]
    private Texture[] textures;

    private int animationStep;
    private bool playing = true;

    [SerializeField]
    private float fps = 5f;

    public Transform target;

    private void Awake()
    {
        lineRenderer= GetComponent<LineRenderer>();
        StartCoroutine(switchTexture());
    }

    private void Update()
    {

        lineRenderer.SetPosition(0,transform.position);
        lineRenderer.SetPosition(1,target.position);
    }

    private IEnumerator switchTexture()
    {
        while (playing)
        {
            animationStep++;
            if (animationStep == textures.Length - 1)
                animationStep = 0;


            lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);
            yield return new WaitForSecondsRealtime(fps);
        }
    }
}

