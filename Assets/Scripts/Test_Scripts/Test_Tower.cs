using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Tower : MonoBehaviour
{
    public Transform target;
    public float range = 15f;

    public string enemyTag = "Enemy";


    public GameObject bulletPrefab;
    public float BulletSpeed = 10.0f;


    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); 
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortesDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy<shortesDistance)
            {
               
                shortesDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortesDistance<=range)
        {
            target = nearestEnemy.transform;
            GameObject bullet = Instantiate(bulletPrefab);

            bullet.transform.position = transform.position+Vector3.up+Vector3.up;
            Vector3 dir = (target.transform.position - transform.position).normalized;
            Rigidbody BulletRigid=bullet.GetComponent<Rigidbody>();
            BulletRigid.velocity = dir * BulletSpeed;

            
            //Debug.Log("근처에 있음");
        }
        else
        {
            target = null;
        }


    }

    void Update()
    {
        if (target == null)
            return;
        
    }

}
