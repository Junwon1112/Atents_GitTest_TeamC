using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAlpha : MonoBehaviour
{
    //전장의 안개 카메라용 스크립트

    public Camera cam;

    private void Awake()
    {
        if (cam == null)
            cam = this.GetComponent<Camera>();

        Initalize();
    }

    private void Initalize()
    {
        cam.clearFlags = CameraClearFlags.Color;
    }

    /// <summary>
    /// 카메라가 씬의 렌더링을 종류한 후 호출되는 함수
    /// </summary>
    private void OnPostRender()
    {
        cam.clearFlags = CameraClearFlags.Nothing;
    }
}
