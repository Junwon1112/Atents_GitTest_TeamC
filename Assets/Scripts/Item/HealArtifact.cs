using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArtifact
{
    float healPerSeconds = 1.0f;
    //float delayTime = 1.0f;
    private float timeLeft = 1.0f;
    private float nextTime = 0.0f;

    public void ArtifactHealing()
    {
        //StartCoroutine(HealingDelay());
        if (Time.time > nextTime)
        {
            nextTime = Time.time + timeLeft;
            GameManager.INSTANCE.PLAYER.GetComponent<IHealth>().TakeHeal(healPerSeconds);
        }
    }
}
