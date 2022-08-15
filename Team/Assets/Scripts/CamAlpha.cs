using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAlpha : MonoBehaviour
{
    public Camera cam;

    private void Awake()
    {
        if(cam == null)
            cam = this.GetComponent<Camera>();

        Initalize();
    }

    private void Initalize()
    {
        cam.clearFlags = CameraClearFlags.Color;
    }

    private void OnPostRender()
    {
        cam.clearFlags = CameraClearFlags.Nothing;
    }
}
