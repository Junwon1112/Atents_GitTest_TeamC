using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMonster : Monster
{

    public override void Update()
    {
        switch (state)
        {
            case MonsterState.Chase:
                ChaseUpdate();
                break;
            case MonsterState.Attack:
                AttackUpdate();
                break;
            case MonsterState.Dead:
            default:
                break;
        }
    }

    //public override void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.layer == 6)    // 테스트불릿의 공격
    //    {
    //        anim.SetTrigger("TakeDamage");
    //        return;
    //    }
    //    if (other.gameObject.CompareTag("Player"))  // 몬스터가 플레이어를 공격하기
    //    {
    //        ChangeState(MonsterState.Attack);
    //        return;
    //    }
    //}

    public override void AttackUpdate()
    {
        if(state == MonsterState.Attack)
        {
            anim.SetTrigger("Attack");
            int randomAttack = Random.Range(0, 5);
            //Debug.Log($"{randomAttack}");
            switch (randomAttack)
            {
                case 0:
                case 1:
                    QuadraAttack();
                    break;
                case 2:
                case 3:
                case 4:
                    Double();
                    break;
            }
        }
    }

    void QuadraAttack()
    {
        anim.SetInteger("AttackType", 0);
        Attack(attackTarget);
        attackCoolTime = attackSpeed;
        return;
    }

    void Double()
    {
        anim.SetInteger("AttackType", 1);
        Attack(attackTarget);
        attackCoolTime = attackSpeed;
        return;
    }
}
