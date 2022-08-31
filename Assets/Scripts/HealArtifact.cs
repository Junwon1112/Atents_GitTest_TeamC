using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealArtifact : MonoBehaviour
{
    public GameObject healingEffect;


    //체력을 회복해주는 아티펙트
    float healPerSeconds = 1.0f;

    float timeLeft = 1.0f;
    float nextTime = 0.0f;

    IHealth PlayerHealth;

    /// <summary>
    /// 일정시간마다 플에이어 체력을 회복
    /// </summary>
    void ArtifactHealing()
    {
        if(Time.time>nextTime && !GameManager.INSTANCE.CAMERASWAP)
        {
            nextTime = Time.time + timeLeft;
            //Debug.Log("힐 아티펙트 발동");
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
