using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Inventory : MonoBehaviour
{
    private void Start()
    {
        ItemFactory.MakeItem(ItemIDCode.HP_potion);
    }
}
