using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public Transform attackTarget = null;
    float attackCoolTime = 1.0f;
    float attackSpeed = 1.0f;
    float Range = 30.0f;
    float closeRange = 2.5f;
    Vector3 targetPosition = new();
    WaitForSeconds oneSecond = new WaitForSeconds(1.0f);
    
    

    public float speed = 10.0f;
    public float lifeTime = 3.0f;
    Rigidbody rigid = null;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rigid.velocity = transform.forward * speed;
        Destroy(this.gameObject, lifeTime);
    }
    void AttackUpdate()
    {
        attackCoolTime -= Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(attackTarget.transform.position - transform.position), 0.1f);
        if (attackCoolTime < 0.0f)
        {
           
            
            attackCoolTime = attackSpeed;
        }
        
    }
    bool SearchPlayer()
    {
        bool result = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, Range, LayerMask.GetMask("Player"));
        if (colliders.Length > 0) 
        {
            Vector3 pos = colliders[0].transform.position;
            
            if (!result && (pos - transform.position).sqrMagnitude < closeRange * closeRange)
            {
                targetPosition = pos;
                result = true;
            }
        }

        return result;
    }
   



}
