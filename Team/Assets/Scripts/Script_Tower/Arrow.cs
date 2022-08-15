using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    float attackPower = 20.0f;

    private void Start()
    {
        StartCoroutine(Del());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IBattle>().TakeDamage(attackPower);
            //Debug.Log("Enemy Hit!!!");
            Destroy(this.gameObject);
        }
    }

    IEnumerator Del()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
