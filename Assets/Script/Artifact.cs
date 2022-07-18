using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    float healPerSeconds = 1.0f;
    float delayTime = 1.0f;

    public float Healing(float playerHP)
    {
        //StartCoroutine(HealingDelay());
        playerHP += healPerSeconds;
        return playerHP;
    }
    //IEnumerator HealingDelay()
    //{
    //    yield return new WaitForSeconds(delayTime);
    //}

}
