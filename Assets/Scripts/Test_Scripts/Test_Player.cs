using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_Player : MonoBehaviour
{
    public float speed = 10.0f;

    Rigidbody rigdbody;
    Vector3 movement;
    Renderer r;

    private void Awake()
    {
        rigdbody = GetComponent<Rigidbody>();
        r= GetComponentInChildren<Renderer>();
        r.material.color = Color.red;
    }

    private void Update()
    {
        Keyboard k = Keyboard.current;
        
        if (!GameManager.INSTANCE.CAMERASWAP)
        {
            if (k.wKey.isPressed )
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
               
            }

            if (k.aKey.isPressed )
            {
                transform.Rotate(Vector3.down * 100.0f * Time.deltaTime);
               
            }

            if (k.dKey.isPressed)
            {
                transform.Rotate(Vector3.up * 100.0f * Time.deltaTime);
                
            }

            if (k.sKey.isPressed)
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
                
            }
            
        }
    }
}
