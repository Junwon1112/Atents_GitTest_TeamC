using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageText : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
  
    }

    private void Start()
    {
        GameManager.INSTANCE.StageChange += textChange;
        textChange();
    }

    void textChange()
    {
        int stage = GameManager.INSTANCE.Stage;


        textMeshProUGUI.text = $"Stage {stage}";
    }
}
