using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWolf : MonoBehaviour
{
    Rigidbody rigid = null;
    Vector3 inputDir = Vector3.zero;
    public float turnSpeed = 10.0f;
    
    public float moveSpeed = 3.0f;
    Quaternion targetRotation = Quaternion.identity;

    Animator anim = null;
    //float inputRotY;
    //float inputRotX;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //anim.SetBool("isMove", true);
        rigid.MovePosition(rigid.position + moveSpeed * Time.fixedDeltaTime * inputDir);
        //rigid.MoveRotation(Quaternion.Lerp(rigid.rotation, Quaternion.Euler(0, inputRot ,0), 0.5f));
        //rigid.MovePosition(rigid.position + moveSpeed * Time.deltaTime * inputDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        //transform.LookAt(inputDir);
        if(inputDir.x != 0 || inputDir.z != 0)
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }



    }

    public void OnmoveInput(InputAction.CallbackContext context)
    {
        Vector3 input;
        input = context.ReadValue<Vector2>();
        inputDir.x = input.x;
        inputDir.y = 0.0f;
        inputDir.z = input.y;
        if (inputDir.sqrMagnitude > 0.0f)    //sqrMagnitude => vector�� �����Ѱ�, root ���길���Ѱ�
        {

            inputDir = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * inputDir;
            // ī�޶��� y�� ȸ���� ���� �и��ؼ� ȸ��
            targetRotation = Quaternion.LookRotation(inputDir);
            //ī�޶� ���¹���������� �Է��� �ٲ�
            
        }
        




        //inputRot = - Mathf.Acos(inputDir.x) * 180 / Mathf.PI - Mathf.Asin(inputDir.y) * 180 / Mathf.PI;


        //if (inputDir.y == 1) 
        //{
        //    inputRot = 0;
        //}
        //else if(inputDir.y == -1)
        //{
        //    inputRot = 180;
        //}
        //if (inputDir.x == 1)
        //{
        //    inputRot = 90;
        //}
        //else if (inputDir.x == -1)
        //{
        //    inputRot = -90;
        //}

        //inputRot = inputRotX + inputRotY;


        //if (context.canceled)
        //{
        //    inputRot = 0;
        //}

    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            anim.SetBool("isAttack", true);
        }else if(context.canceled)
        {
            anim.SetBool("isAttack", false);
        }
    }
}
