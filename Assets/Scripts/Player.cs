using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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
        if (!GameManager.INSTANCE.CAMERASWAP)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
               
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.down * 100.0f * Time.deltaTime);
               
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(Vector3.up * 100.0f * Time.deltaTime);
                
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector3.back * speed * Time.deltaTime);
                
            }
            
        }
    }
}
