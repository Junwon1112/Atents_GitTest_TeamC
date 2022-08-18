using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_TowerAttack : MonoBehaviour
{
    //GameObject Target = null;

    public GameObject bullet = null;
    public float BulletSpeed = 10.0f;

    private float BulletDelayMax = 1.0f;
    private float BulletDelay = 0.0f;

    public Transform BulletPoint = null;

    private List<GameObject> EnemyList = new List<GameObject>();
    private void FixedUpdate()
    {

        if (!GameManager.INSTANCE.CAMERASWAP)
        {

            if (BulletDelay < BulletDelayMax)
            {
                BulletDelay += Time.fixedDeltaTime;
            }

            if (EnemyList.Count > 0)
            {
                transform.LookAt(EnemyList[0].transform);
            }

            /*if(Target!=null && BulletDelay>BulletDelayMax)
            {
                GameObject b = Instantiate(bullet);
                b.transform.position = BulletPoint.transform.position;
                Vector3 dir = (Target.transform.position - BulletPoint.transform.position).normalized;
                Rigidbody BulletRigid = b.GetComponent<Rigidbody>();
                BulletRigid.velocity = dir * BulletSpeed;

                BulletDelay = 0.0f;
            }*/

            if (EnemyList.Count > 0 && BulletDelay > BulletDelayMax && EnemyList[0].activeSelf)
            {
                GameObject b = Instantiate(bullet);
                b.transform.position = BulletPoint.transform.position;
                Vector3 dir = (EnemyList[0].transform.position - BulletPoint.transform.position).normalized;
                Rigidbody BulletRigid = b.GetComponent<Rigidbody>();
                BulletRigid.velocity = dir * BulletSpeed;

                BulletDelay = 0.0f;
            }
            if (EnemyList.Count > 0 && !EnemyList[0].activeSelf)
            {
                EnemyList.Remove(EnemyList[0]);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Debug.Log("ÀûÀÌ µé¾î¿È");

            EnemyList.Add(other.gameObject);


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyList.Remove(other.gameObject);

        }
    }
}
