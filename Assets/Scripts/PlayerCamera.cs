using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera[] arrCam;

    int Count = 2;
    int Now = 0;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Now++;

            if(Now>=Count)
            {
                Now = 0;
            }

            for(int i=0; i<arrCam.Length; i++)
            {
                arrCam[i].enabled = (i == Now);
            }
            GameManager.INSTANCE.TowerSwap();
        }
    }
}
