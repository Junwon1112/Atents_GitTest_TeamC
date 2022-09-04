using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    //나이프 오브젝트에 들어가는 스크립트
    float attackPower = 15.0f;
    //Rigidbody rb;
    //Collider col;
    public GameObject ps;

    private void Start()
    {
        //col = GetComponent<Collider>();
        //rb = GetComponent<Rigidbody>();
        StartCoroutine(Del());
    }

    //적과 닿으면 데미지를 주면서 제거
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IBattle battle = other.GetComponent<IBattle>();
            if (battle != null)
            {
                battle.TakeDamage(attackPower);
            }
            //rb.velocity = Vector3.zero;
            //col.enabled = false;
            //transform.parent=other.transform;
            GameObject psObject= Instantiate(ps,transform.position,Quaternion.identity,null);
            psObject.GetComponent<ParticleSystem>().Play();
            
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
