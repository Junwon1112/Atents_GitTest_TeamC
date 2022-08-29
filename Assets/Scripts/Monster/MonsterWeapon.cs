using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour
{
    //몬스터의 무기에 있는 스크립트

    /// <summary>
    /// 플레이어에게 닿으면 데미지
    /// </summary>
    /// <param name="other">플레이어만 잡게 태그를 설정함</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IBattle battle = other.GetComponent<IBattle>();
            if (battle != null)
            {
                battle.TakeDamage(10.0f);
            }
            
        }
    }

}

