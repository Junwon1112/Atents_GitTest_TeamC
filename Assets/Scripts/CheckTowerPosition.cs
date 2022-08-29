using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTowerPosition : MonoBehaviour
{
    private bool wallState = false;
    private bool towerZone=false;
    public bool WallState
    {
        get { return wallState; }
    }

    public bool TowerZone
    {
        get { return towerZone; }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Wall"))
        {
            wallState = true;
        }

        if(other.CompareTag("TowerSpawnRange"))
        {
            towerZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Wall"))
        {
            wallState = false;
        }

        if(other.CompareTag("TowerSpawnRange"))
        {
            towerZone = false;
        }
    }
}
