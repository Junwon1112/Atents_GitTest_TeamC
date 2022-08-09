using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towel : MonoBehaviour
{
    
    public float range;
    public GameObject Target;
    public Animator anim;
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.2f);
        anim = GetComponent<Animator>();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.clear;
        Gizmos.DrawSphere(this.gameObject.transform.position, range);
    }
    public void Attack()
    {
        anim.SetInteger("NewState", 2);
    }

    public void Idle()
    {
        anim.SetInteger("NewState", 1);
    }

    // Update is called once per frame
    void UpdateTarget()
    {
        if (Target == null)
        {
            GameObject[] Monsters = GameObject.FindGameObjectsWithTag("Monster");
            float shortestDistance = Mathf.Infinity;
            GameObject nearestMonster = null;
            foreach (GameObject Monster in Monsters)
            {
                float DistanceToMonsters = Vector3.Distance(transform.position, Monster.transform.position);

                if (DistanceToMonsters < shortestDistance)
                {
                    shortestDistance = DistanceToMonsters;
                    nearestMonster = Monster;
                }
                if (nearestMonster != null && shortestDistance <= range)
                {
                    Target = nearestMonster;
                    Attack();
                }
                else
                {
                    Idle();
                    Target = null;
                }
            }
        }
        else if(Target == null)
        {
            float DistanceToMonsters = Vector3.Distance(transform.position, Target.transform.position);
            if(DistanceToMonsters > range)
            {
                Idle();
                Target = null;
            }
        }

    }
    void AttackTarget()
    {

    }
}
