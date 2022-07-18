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

    public float moveSpeed = 2.0f;
    public float turnSpeed = 1.0f;

    Vector3 inputDir = Vector3.zero;
    float inputSide = 0.0f;

    Quaternion targetRotation = Quaternion.identity;

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
    }

    private void OnDisable()
    {       
        actions.Player.Move.canceled -= OnMove;
        actions.Player.Move.performed -= OnMove;
        actions.Player.Disable();
    }
    private void Update()
    {
        move();
    }

    private void move()
    {
        rigid.MovePosition(rigid.position
            + moveSpeed * Time.fixedDeltaTime * (inputDir.y * transform.forward + inputSide * transform.right));        
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();
    }

    
}
