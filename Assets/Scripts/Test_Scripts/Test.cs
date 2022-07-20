using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{

    
    // Update is called once per frame
    void Update()
    {
        Keyboard k = Keyboard.current;
        if(k.digit1Key.wasPressedThisFrame)
        {
            GameManager.INSTANCE.PLAYER.GetComponent<IHealth>().TakeDamage(10);
        }

        //Keyboard b = Keyboard.current;
        //if (k.digit2Key.wasPressedThisFrame)
        //{
        //    GameManager.INSTANCE.PLAYER.GetComponent<IHealth>().TakeHeal(10);
        //}
    }
}
