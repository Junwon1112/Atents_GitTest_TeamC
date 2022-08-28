using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Inventory : MonoBehaviour
{
    private void Start()
    {
        ItemFactory.MakeItem(ItemIDCode.HP_potion);
        ItemFactory.MakeItem(ItemIDCode.HP_potion); ItemFactory.MakeItem(ItemIDCode.HP_potion); ItemFactory.MakeItem(ItemIDCode.HP_potion);
        ItemFactory.MakeItem(ItemIDCode.Healing_Artifact, new(1,0,0), true);
    }
}
