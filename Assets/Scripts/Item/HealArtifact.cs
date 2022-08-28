using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArtifact : MonoBehaviour
{
    public GameObject healingEffect;

    float healPerSeconds = 1.0f;
    private float timeLeft = 1.0f;
    private float nextTime = 0.0f;

    public void ArtifactHealing()
    {
        if (Time.time > nextTime)
        {
            nextTime = Time.time + timeLeft;
            GameManager.INSTANCE.PLAYER.GetComponent<IHealth>().TakeHeal(healPerSeconds);
        }
    }

    private void Update()
    {
        ArtifactHealing();
    }
}
