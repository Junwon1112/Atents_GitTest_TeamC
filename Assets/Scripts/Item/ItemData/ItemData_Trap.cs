using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Trap용 데이터 파일 만드는 스크립트
/// </summary>
[CreateAssetMenu(fileName = "New Trap", menuName = "Scriptable Object/Item Data - Trap", order = 3)]
public class ItemData_Trap : ItemData, IUsable
{
    [Header("트렙 데이터")]
    //float trapDamage = 20.0f;
    public GameObject trap;
    Transform trapPos;

    public void Use(GameObject target = null)
    {
        trapPos = GameObject.FindObjectOfType<FindTrapPos>().transform;
        Instantiate(trap,trapPos);
    }
}