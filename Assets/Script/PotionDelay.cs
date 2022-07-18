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
            // ���� ���
            StartCoroutine(DrinkPotionDelay());
            playerHP += potionHealPoint;
           
            potionNum--;
        }
        else
        {
            Debug.Log("���� ��Ÿ���� ���ҽ��ϴ�");
            
                // ���� ��� �Ұ�
        }
        return playerHP;
    }
    IEnumerator DrinkPotionDelay()
        {
            yield return new WaitForSeconds(delayTime);
            isDelay = false;
        }
}
