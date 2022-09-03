using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour
{
    public WeaponType type = WeaponType.NomalWeapon;

    public float attackPower = 10.0f; //공격력
    public float bossPower = 2.0f;  // 보스 공격력 배수


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
                if(type == WeaponType.NomalWeapon)
                {
                    battle.TakeDamage(attackPower);
                }

                if(type==WeaponType.BossWeapon)  // 무기 타입이 보스무기면 최종 공격력 = 공격력 * 공격력 배수
                {
                    battle.TakeDamage(attackPower * bossPower);
                }
            }
            return;
            
        }
    }

}

