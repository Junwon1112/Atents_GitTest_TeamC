using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    PlayerInputActions actions;
    Rigidbody rigid;
    Animator anim = null;

    public float moveSpeed = 5.0f;
    public float turnSpeed = 10.0f;    

    Vector3 inputDir = Vector3.zero;    

    Quaternion targetRotation = Quaternion.identity;

    public Transform maincamera;
    private void Awake()
    {
        actions = new();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += OnMove;
        actions.Player.Move.canceled += OnMove;
        actions.Player.Look.performed += OnLook;
    }    

    private void OnDisable()
    {
        //actions.Player.Look.performed -= OnLook;
        actions.Player.Move.canceled -= OnMove;
        actions.Player.Move.performed -= OnMove;
        actions.Player.Disable();
    }
    private void FixedUpdate()
    {
        move();
    }
    private void move()
    {
        if (inputDir.sqrMagnitude > 0.0f)
        {
            rigid.MovePosition(rigid.position
            + moveSpeed * Time.fixedDeltaTime * (inputDir.y * transform.forward));
                        
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }    
    
    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        inputDir.x = input.x;
        inputDir.y = 0.0f;
        inputDir.z = input.y;

        if (inputDir.sqrMagnitude > 0.0f)
        {            
            inputDir = Quaternion.Euler(0, maincamera.transform.rotation.eulerAngles.y, 0) * inputDir;
            
            targetRotation = Quaternion.LookRotation(inputDir);
            
            inputDir.y = -2f;
        }
        //Debug.Log(input);
    }
    private void OnLook(InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();        
    }
}
