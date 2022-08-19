using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Player : MonoBehaviour
{
    public Transform camTr;

    float h;
    float v;
    float mouseX;
    float mouseY;

    Vector3 velocity;

    private void Awake()
    {
        Camera cam = GetComponentInChildren<Camera>();
        camTr = cam.transform;
    }

    private void Update()
    {
        GetPlayerInput();
        
        MoveAndRotate();
    }

    void GetPlayerInput()
    {
        mouseX = Mouse.current.position.ReadValue().x;
        mouseY = Mouse.current.position.ReadValue().y;
    }

    private void CalculateVelocity()
    {
        velocity = ((transform.forward * v) + (transform.right * h)) * Time.deltaTime;
        
    }

    private void MoveAndRotate()
    {
        // Rotate
        transform.Rotate(Vector3.up * mouseX);
        camTr.Rotate(Vector3.right * -mouseY);

        // Move
        transform.Translate(velocity, Space.World);
    }

}
