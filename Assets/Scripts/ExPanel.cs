using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ExPanel : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    public void TextChange(int number)
    {
        if(number==0)
        {
            textMeshProUGUI.text = "나이프 타워\n" +
                "타워사거리 : 짧음\n" +
                "공격속도 : 빠름\n";
        }else if(number==1)
        {
            textMeshProUGUI.text = "화살 타워\n" +
                "타워사거리 : 길다\n" +
                "공격속도 : 느림\n";
        }
    }

}
