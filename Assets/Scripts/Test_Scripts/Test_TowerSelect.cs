using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Test_TowerSelect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public int ButtonNumber = 0;
    public ExPanel exPanel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        exPanel.gameObject.SetActive(true);
        exPanel.TextChange(ButtonNumber);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        exPanel.gameObject.SetActive(false);
    }

    public void Select()
    {
        GameManager.INSTANCE.MOUSE.GetComponent<Mouse_Control>().ObjectSwap(ButtonNumber);
    }
}
