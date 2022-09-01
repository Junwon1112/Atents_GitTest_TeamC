using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveText : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;


    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        
    }

    private void Start()
    {
        GameManager.INSTANCE.WaveChange += textChange;
        textChange();
    }

    void textChange()
    {
        int wave = GameManager.INSTANCE.Wave;
        int maxWave = GameManager.INSTANCE.MaxWave;

        textMeshProUGUI.text = $"Wave {wave}/{maxWave}";
    }


}
