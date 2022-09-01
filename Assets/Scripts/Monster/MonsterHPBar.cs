using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHPBar : MonoBehaviour
{
    //몬스터의 Hp를 표시해주는 스크립트

    IHealth target;
    Transform fillPivot;

    private void Awake()
    {
        target = GetComponentInParent<IHealth>();
        target.onHealthChange += SetHP_Value;
        fillPivot = transform.Find("FillPivot");
    }

    void SetHP_Value()
    {
        if (target != null)
        {
            float ratio = target.HP / target.MaxHP;
            fillPivot.localScale = new Vector3(ratio, 1, 1);
        }
    }
    /// <summary>
    /// hp바가 회전하지 않고 플레이 하는 사람이 볼수있게 업데이트
    /// </summary>
    private void LateUpdate()
    {
        transform.forward = -Camera.main.transform.forward;
    }
}

