using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelect : MonoBehaviour
{
    public int ButtonNumber = 0;
    public void Select()
    {
        GameManager.INSTANCE.MOUSE.GetComponent<Mouse>().ObjectSwap(ButtonNumber);
    }
}
