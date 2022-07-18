using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDelay : MonoBehaviour
{
    public bool isDelay;
    public float delayTime = 5.0f;
    public float accumTime;
    public float potionHealPoint = 20.0f;
    public int potionNum = 5;

    Player player = new Player();

    
    //public Action onHealthChange { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    private void Update()
    {
        
    }

    public float DrinkPotion(float playerHP)
    {

        if (isDelay == false)
        {
            isDelay = true;
            // 포션 사용
            StartCoroutine(DrinkPotionDelay());
            playerHP += potionHealPoint;
           
            potionNum--;
        }
        else
        {
            Debug.Log("아직 쿨타임이 남았습니다");
            
                // 포션 사용 불가
        }
        return playerHP;
    }
    IEnumerator DrinkPotionDelay()
        {
            yield return new WaitForSeconds(delayTime);
            isDelay = false;
        }
}
