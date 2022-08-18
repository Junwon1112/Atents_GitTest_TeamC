using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPotion : MonoBehaviour
{
    bool isDelay=false;
    float delayTime = 5.0f;
    float accumTime;
    float PotionHealPoint = 20.0f;


    IHealth PlayerHealth;
    void Start()
    {
        PlayerHealth=GameManager.INSTANCE.PLAYER.GetComponent<IHealth>();
    }
    void Healing()
    {
        PlayerHealth.TakeHeal(PotionHealPoint);
    }

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
            Debug.Log("���� ��Ÿ���� ���ҽ��ϴ�");
        }
    }

    IEnumerator DrinkPotionDelay()
    {
        yield return new WaitForSeconds(delayTime);
        isDelay = false;
    }
    
}
