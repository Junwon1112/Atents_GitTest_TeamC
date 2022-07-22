using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IBattle battle = other.GetComponent<IBattle>();
            Debug.Log("OnTriggerPlayer" + other.name);
            if (battle != null)
            {
                battle.TakeDamage(10.0f);
            }
            return;
        }
    }
}
