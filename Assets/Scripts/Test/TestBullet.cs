using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour, IBattle, IHealth
{
    public float damage = 10.0f;

    float hp = 1.0f;
    float maxHP = 1.0f;

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

    // ÀüÅõ¿ë

    public float attackPower = 20.0f;
    public float defencePower = 0.0f;

    public float AttackPower { get => attackPower; }

    public float DefencePower { get => defencePower; }

    private void Start()
    {
        //StartCoroutine(Del());
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<IBattle>().TakeDamage(attackPower);
            //Debug.Log($"Hit!");
            //Destroy(this.gameObject);
        }
    }

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

    }

    //IEnumerator Del()
    //{
    //    yield return new WaitForSeconds(3.0f);
    //    Destroy(gameObject);
    //}
}
