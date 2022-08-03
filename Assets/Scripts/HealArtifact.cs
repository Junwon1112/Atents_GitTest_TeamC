using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArtifact : MonoBehaviour
{
    float healPerSeconds = 1.0f;

    float timeLeft = 1.0f;
    float nextTime = 0.0f;

    IHealth PlayerHealth;

    void ArtifactHealing()
    {
        if(Time.time>nextTime && !GameManager.INSTANCE.CAMERASWAP)
        {
            nextTime = Time.time + timeLeft;
            Debug.Log("Èú ¾ÆÆ¼ÆåÆ® ¹ßµ¿");
            PlayerHealth.TakeHeal(healPerSeconds);
        }
    }

    private void Start()
    {
         PlayerHealth=GameManager.INSTANCE.PLAYER.GetComponent<IHealth>();   
    }

    // Update is called once per frame
    void Update()
    {
        ArtifactHealing();
    }
}
