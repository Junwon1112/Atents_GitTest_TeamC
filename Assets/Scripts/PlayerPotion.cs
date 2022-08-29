using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPotion : MonoBehaviour
{
    //포션에 들어가는 스크립트

    bool isDelay=false;
    float delayTime = 5.0f;
    float accumTime;
    float PotionHealPoint = 20.0f;


    IHealth PlayerHealth;
    void Start()
    {
        PlayerHealth=GameManager.INSTANCE.PLAYER.GetComponent<IHealth>();
    }

    /// <summary>
    /// 플레이어 체력을 회복시켜주는 함수
    /// </summary>
    void Healing()
    {
        PlayerHealth.TakeHeal(PotionHealPoint);
    }

    /// <summary>
    /// Healing함수를 실행시켜주면서 쿨타임 코루틴을 실행시켜주는 함수
    /// </summary>

    public void OnDrinkPotion()
    {
        if(isDelay==false)
        {
            isDelay=true;
            StartCoroutine(DrinkPotionDelay());
            Healing();
        }
        else
        {
            Debug.Log("아직 쿨타임이 남았습니다");
        }
    }
    /// <summary>
    /// 쿨타임용 IEnumerator
    /// </summary>
    /// <returns>delayTime뒤에 isDelay를 false로 만듬</returns>
    IEnumerator DrinkPotionDelay()
    {
        yield return new WaitForSeconds(delayTime);
        isDelay = false;
    }
    
}
