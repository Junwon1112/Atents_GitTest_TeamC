using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPotion : MonoBehaviour
{
    IHealth target;

    public bool isDelay;
    public float delayTime = 5.0f;
    public float accumTime;
    public float potionHealPoint = 20.0f;
    //public int potionNum = 5;

    public void Healing()
    {
        GameManager.INSTANCE.PLAYER.GetComponent<IHealth>().TakeHeal(potionHealPoint);
    }
}
