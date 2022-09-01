using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_HpBar : MonoBehaviour
{
    //플레이어 체력을 표시해주는 스크립트

    IHealth target;
    Image hp;
    private void Start()
    {
        hp=GetComponentInChildren<Image>();
        target=GameManager.INSTANCE.PLAYER.GetComponent<IHealth>();
        target.onHealthChange += SetHp_Value;
        //gameObject.SetActive(false);
    }

    /// <summary>
    /// 플레이어의 onHealthChange델리게이트에 할당해서 hp가 변할때만 hp바를 움직일수 있게 하는 함수
    /// </summary>
    void SetHp_Value()
    {
        if(target!=null)
        {
            float ratio = target.HP / target.MaxHP;
            hp.fillAmount = ratio;
        }
    }    
}
