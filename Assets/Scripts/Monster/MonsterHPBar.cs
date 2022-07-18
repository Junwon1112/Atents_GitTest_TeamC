using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHPBar : MonoBehaviour
{
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

    // ������ -> �׻� ī�޶� �������� ���ֺ��� ������Ʈ    
    private void LateUpdate()
    {
        transform.forward = -Camera.main.transform.forward;
    }
}

