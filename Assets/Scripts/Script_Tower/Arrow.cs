using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //화살 오브젝트에 들어가는 스크립트

    float attackPower = 20.0f;

    private void Start()
    {
        StartCoroutine(Del());
    }

    //적과 닿으면 데미지를 주면서 제거
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IBattle>().TakeDamage(attackPower);
            //Debug.Log("Enemy Hit!!!");
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// isTrigger로 설정했기때문에 벽을 통과하여 무한이 날라가는 것을 방지하기위해 3초뒤 자동으로 제거
    /// </summary>
    /// <returns></returns>
    IEnumerator Del()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}
