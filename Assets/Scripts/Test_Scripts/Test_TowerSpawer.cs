using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_TowerSpawer : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        /*if (other.CompareTag("TowerSpawnerZone"))
        {
            Debug.Log("벽위에있음");
        }*/
        Debug.Log("벽위에있음");
    }

}
