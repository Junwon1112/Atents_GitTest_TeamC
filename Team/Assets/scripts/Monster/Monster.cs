using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IBattle, IHealth
{
    GameObject weapon;

    NavMeshAgent nav;
    Animator anim;

    private Vector3 quadPosition;
    Transform quad;

    public Transform target;

    MonsterState state = MonsterState.Chase;

    // ���ݿ�
    public float attackSpeed = 1.0f;
    public float attackCoolTime = 1.0f;
    IBattle attackTarget;

    // HP��

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

    // ������

    public float attackPower = 10.0f;
    public float defencePower = 5.0f;

    public float AttackPower { get => attackPower; }
    public float DefencePower { get => defencePower; }

    // ���
    bool isDead = false;

    private void Awake()
    {
        weapon = GetComponentInChildren<FindWeapon>().gameObject;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        quad = transform.Find("Goblin_Quad");
    }

    // ���� �� Ÿ�� �÷��̾�� ��ȯ
    GameObject Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        target = Player.transform;
    }

    private void Update()
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
        quadPosition = new Vector3(quad.position.x, transform.position.y, quad.position.z);
        quad.transform.LookAt(quadPosition);
    }

    void ChaseUpdate()
    {
        nav.SetDestination(target.position);
        return;
    }
    void AttackUpdate()
    {
        attackCoolTime -= Time.deltaTime;

        if (attackCoolTime < 0.0f)
        {
            anim.SetTrigger("Attack");
            Attack(attackTarget);
            attackCoolTime = attackSpeed;
            return;
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)    // �׽�Ʈ�Ҹ��� ����
        {
            anim.SetTrigger("TakeDamage");
            return;
        }
        if (other.gameObject.CompareTag("Player"))  // ���Ͱ� �÷��̾ �����ϱ�
        {
            //attackTarget = other.GetComponent<IBattle>();     // �ݶ��̴��� ������ �ٷ� ���ݹߵ�
            ChangeState(MonsterState.Attack);
            return;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ChangeState(MonsterState.Chase);
            return;
        }

    }

    void ChangeState(MonsterState newState)
    {
        if (isDead)
        {
            return;
        }

        switch (state)
        {
            case MonsterState.Chase:
                nav.isStopped = true;
                break;
            case MonsterState.Attack:
                nav.isStopped = true;
                attackTarget = null;
                break;
            case MonsterState.Dead:
                nav.isStopped = true;
                isDead = false;
                break;
            default:
                break;
        }
        switch (newState)
        {
            case MonsterState.Chase:
                nav.isStopped = false;
                break;
            case MonsterState.Attack:
                nav.isStopped = true;
                attackCoolTime = attackSpeed;
                break;
            case MonsterState.Dead:
                DiePresent();
                break;
            default:
                break;
        }
        state = newState;
        anim.SetInteger("MonsterState", (int)state);
    }

    void DiePresent()
    {
        //gameObject.layer = LayerMask.NameToLayer("Corpse");
        anim.SetBool("Dead", true);
        anim.SetTrigger("Die");
        isDead = true;
        nav.isStopped = true;
        nav.velocity = Vector3.zero;
        HP = 0;
        StartCoroutine(DeadEffect());
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
        float finalDamage = damage - defencePower;
        if (finalDamage < 1.0f)
        {
            finalDamage = 1.0f;
        }
        HP -= finalDamage;

        if (HP > 0.0f)
        {
            anim.SetTrigger("TakeDamage");
            attackCoolTime = attackSpeed;
        }
        else
        {
            Die();
        }
        //Debug.Log($"MonsterHP : {hp}");
    }

    private void Die()
    {
        if (isDead == false)
        {
            GameManager.INSTANCE.MONSTERLIVECOUNT--;
            ChangeState(MonsterState.Dead);
        }
    }

    IEnumerator DeadEffect()
    {
        yield return new WaitForSeconds(1.0f);
        Collider[] colliders = GetComponents<Collider>();
        foreach (var col in colliders)
        {
            col.enabled = false;
        }
        nav.enabled = false;
        gameObject.SetActive(false);
        //Destroy(this.gameObject, 1.0f);
    }

    public void TakeHeal(float heal)
    {
        Debug.Log("���� ����");
    }
}
