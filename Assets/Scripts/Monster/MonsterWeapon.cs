using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour
{
    public WeaponType type = WeaponType.NomalWeapon;

    public float attackPower = 10.0f;   // 공격력
    public float bossPower = 2.0f;      // 보스 공격력 배수

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IBattle battle = other.GetComponent<IBattle>();
            if (battle != null)
            {
                if( type == WeaponType.NomalWeapon)
                {
                    battle.TakeDamage(attackPower);
                }

                if( type == WeaponType.BossWeapon)  // 무기 타입이 보스무기면 최종 공격력 = 공격력 * 공격력 배수
                {
                    battle.TakeDamage(attackPower * bossPower);
                }
            }
            return;
        }
    }
}
