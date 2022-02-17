using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrap : MonoBehaviour
{
    bool isWrappingX = false;
    bool isWrappingY = false;

    Renderer renderers;

    void Start()
    {
        renderers = GetComponent<MeshRenderer>();
    }

    public void Update()
    {
        ScreenWrap();
    }

    bool CheckRenderers()
    {
        if (renderers.isVisible)
        {
            return true;
        }
        return false;
    }



    void ScreenWrap()
    {
        var isVisible = CheckRenderers();

        if (isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }

        if (isWrappingX && isWrappingY)
        {
            return;
        }

        var cam = Camera.main;
        var newPosition = transform.position;
        var viewportPosition = cam.WorldToViewportPoint(transform.position);


        if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;

            isWrappingX = true;
        }

        if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            newPosition.y = -newPosition.y;

            isWrappingY = true;
        }

        transform.position = newPosition;
    }
}
