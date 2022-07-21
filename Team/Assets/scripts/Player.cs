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

    private bool state;
    public GameObject TopView;

    private void Awake()
    {
        actions = new();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        state = true;
    }
   
    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += OnMove;
        actions.Player.Move.canceled += OnMove;
        actions.Player.ViewChange.performed += OnView;        
    }    

    private void OnDisable()
    {       
        actions.Player.ViewChange.performed -= OnView;
        actions.Player.Move.canceled -= OnMove;
        actions.Player.Move.performed -= OnMove;
        actions.Player.Disable();
    }    

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        inputDir.x = input.x;
        inputDir.y = 0.0f;
        inputDir.z = input.y;
      
        inputDir = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * inputDir;
        targetRotation = Quaternion.LookRotation(inputDir);        
       
        //Debug.Log(input);
    }

    private void Update()
    {      
        rigid.MovePosition(moveSpeed * Time.deltaTime * inputDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);        
    }

    private void OnView(InputAction.CallbackContext _)
    {        
            if(state == true)
            {
                TopView.SetActive(false);
                state = false;
            }
            else
            {
                TopView.SetActive(true);
                state = true;
            }              
    }
}
