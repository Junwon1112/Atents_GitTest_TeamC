using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPotion : MonoBehaviour
{

    public bool isDelay;
    public float delayTime = 5.0f;
    public float accumTime;
    public float potionHealPoint = 20.0f;
    //public int potionNum = 5;

    private void Update()
    {
        Keyboard b = Keyboard.current;
        if (b.digit2Key.wasPressedThisFrame)
        {
            OnDrinkPotion();
        }
    }

    public void Healing()
    {
        GameManager.INSTANCE.PLAYER.GetComponent<IHealth>().TakeHeal(potionHealPoint);
    }

    public void OnDrinkPotion(/*InputAction.CallbackContext context*/)
    {
        if (isDelay == false)
        {
            isDelay = true;
            // ���� ���
            StartCoroutine(DrinkPotionDelay());
            Healing();

            //potionNum--;
        }
        else
        {
            Debug.Log("���� ��Ÿ���� ���ҽ��ϴ�");

            // ���� ��� �Ұ�
        }
    }
    IEnumerator DrinkPotionDelay()
    {
        yield return new WaitForSeconds(delayTime);
        isDelay = false;
    }


}
