using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towel : MonoBehaviour
{
    public GameObject bullet = null;
    public float range;
    public GameObject Target = null;
    public Animator anim;
    void Start()
    {
        InvokeRepeating("Updatetarget", 0f, 0.2f);
        anim = GetComponent<Animator>();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.clear;
        Gizmos.DrawSphere(this.gameObject.transform.position, range);
    }
    public void Attack()
    {
        anim.SetInteger("TowelAnimSate", 2);
    }

    public void Idle()
    {
        anim.SetInteger("TowelAnimSate", 1);
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
    }
}
