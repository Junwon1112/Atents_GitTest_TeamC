using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossText : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;

    float sinDelta = 0.0f;
    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        sinDelta+=Time.deltaTime;
        textMeshProUGUI.alpha = (Mathf.Cos(sinDelta*10.0f) + 1) * 0.5f;

    }
}
