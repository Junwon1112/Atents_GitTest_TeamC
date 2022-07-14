using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject obj = null;
    Vector3 Delta = Vector3.zero;
    public float cameraSpeed = 2.0f;

    private void Awake()
    {
        Delta = this.transform.position - obj.transform.position;
    }

    private void FixedUpdate()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        //transform.position += Vector3.Lerp(transform.position, Delta * Time.fixedDeltaTime, 0.5f);
        transform.position = Vector3.Lerp(transform.position, obj.transform.position + Delta, cameraSpeed *Time.fixedDeltaTime);
    }
}
