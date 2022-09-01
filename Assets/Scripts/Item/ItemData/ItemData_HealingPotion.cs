using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HealingPotion용 데이터 파일 만드는 스크립트
/// </summary>
[CreateAssetMenu(fileName = "New HealingPotion", menuName = "Scriptable Object/Item Data - HealingPotion", order = 2)]
public class ItemData_HealingPotion : ItemData, IUsable
{
    [Header("힐링포션 데이터")]
    float healPoint = 20.0f;
    
    public void Use(GameObject target = null)
    {
        IHealth health = target.GetComponent<IHealth>();
        if (health != null)
        {
            //health.HP += healPoint;
            health.TakeHeal(healPoint);
            Debug.Log($"{itemName}을 사용했습니다. HP가 {healPoint} 회복되었습니다. 현재 HP는 {health.HP}입니다.");
        }
    }
}
