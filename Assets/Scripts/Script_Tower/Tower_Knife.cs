using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Knife : Tower_Archer
{
    //나이프타워에 들어가는 스크립트 행동은 Tower_Archer와 유사하기 때문에 상속받음

    //공격속도에만 차이를 두었음
    private void Start()
    {
        BulletDelayMax = 0.3f;
        GameManager.INSTANCE.towerSwapDelegate += LayerChange;
    }
}
