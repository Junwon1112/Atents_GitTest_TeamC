using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyChange : MonoBehaviour
{
    //돈 표시용 스크립트
    TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        GameManager.INSTANCE.PLAYER.GetComponent<PlayerWolf>().MoneyChange += Change;
        Change();
    }

    void Change()
    {
        textMeshProUGUI.text = $"{GameManager.INSTANCE.PLAYER.GetComponent<PlayerWolf>().MONEY}";
    }
}
