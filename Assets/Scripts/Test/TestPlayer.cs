using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour, IHealth, IBattle
{
    public float attackSpeed = 1.0f;
    public float attackCoolTime = 1.0f;
    IBattle attackTarget;


    public float hp = 100.0f;
    float maxHP = 100.0f;

    public float HP
    {
        get => hp;
        private set
        {
            hp = Mathf.Clamp(value, 0.0f, maxHP);
            onHealthChange?.Invoke();
        }
    }

    public float MaxHP { get => maxHP; }

    public System.Action onHealthChange { get; set; }



    public float attackPower = 10.0f;
    public float defencePower = 5.0f;

    public float AttackPower { get => attackPower; }
    public float DefencePower { get => defencePower; }



    public void Attack(IBattle target)
    {
        if (target != null)
        {
            float damage = AttackPower;
            target.TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        float finalDamage = damage - defencePower;
        if (finalDamage < 1.0f)
        {
            finalDamage = 1.0f;
        }
        HP -= finalDamage;

        if (HP < 0.0f)
        {
            Destroy(this.gameObject);
        }
        Debug.Log($"HP : {hp}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {

        }
    }

    void AttackUpdate()
    {

        if (attackCoolTime < 0.0f)
        {
            Attack(attackTarget);
            attackCoolTime = attackSpeed;
            return;
        }


    }
}
