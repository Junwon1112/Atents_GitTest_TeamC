using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_HpBar : MonoBehaviour
{
    IHealth target;
    Image hp;
    private void Start()
    {
        hp=GetComponentInChildren<Image>();
        target=GameManager.INSTANCE.PLAYER.GetComponent<IHealth>();
        target.onHealthChange += SetHp_Value;
        gameObject.SetActive(false);
    }

    void SetHp_Value()
    {
        if(target!=null)
        {
            float ratio = target.HP / target.MAXHP;
            hp.fillAmount = ratio;
        }
    }    
}
