using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            SceneManager.LoadScene(0);
        }
    }
}
