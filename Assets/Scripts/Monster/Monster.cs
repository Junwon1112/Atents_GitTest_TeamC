using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour, IBattle, IHealth
{
    //플레이어를 추격하는 몬스터 스크립트

    GameObject weapon;

    protected NavMeshAgent nav;
    protected Animator anim;

    

    public Transform target;

    public MonsterState state = MonsterState.Chase;
    public MonsterType type = MonsterType.Nomal;
    

    // 공격용
    public float attackSpeed = 1.0f;
    public float attackCoolTime = 1.0f;
    public IBattle attackTarget;

    // HP용

    public float hp = 100.0f;
    float maxHP = 100.0f;

    //미니맵용
    protected Vector3 quadPosition;
    protected Transform quad;

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

    // 전투용

    public float attackPower = 10.0f;
    public float defencePower = 5.0f;

    public float AttackPower { get => attackPower; }
    public float DefencePower { get => defencePower; }

    // 사망
    bool isDead = false;

    private void Awake()
    {
        weapon = GetComponentInChildren<FindWeapon>().gameObject;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        quad = transform.Find("Goblin_Quad");
    }

    // 스폰 후 타겟 플레이어로 변환
    protected GameObject Player;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        target = Player.transform;
        if(type==MonsterType.Nomal)
        {
            maxHP = 100;
            HP = maxHP;
        }else if(type==MonsterType.Boss)
        {
            maxHP = 500;
            HP = maxHP;
        }
    }

    public virtual void Update()
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
        quadPosition = new Vector3(quad.position.x, transform.position.y, quad.position.z); //미니맵고정용
        quad.transform.LookAt(quadPosition); //미니맵고정용
    }


    /// <summary>
    /// 플레이어 추격용 함수
    /// </summary>
    public void ChaseUpdate()
    {
        nav.SetDestination(target.position);
        return;
    }

    /// <summary>
    /// 플레이어 공격용함수
    /// </summary>
    /// <returns>어택쿨타임이 0.0f보다 작아지면 애니메이션재생 쿨타임초기화 Attack함수 실행</returns>
    public virtual void AttackUpdate()
    {
        if (type != MonsterType.Boss)
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


    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)    // 테스트불릿의 공격
        {
            anim.SetTrigger("TakeDamage");
            return;
        }
        if (other.gameObject.CompareTag("Player"))  // 몬스터가 플레이어를 공격하기
        {
            //attackTarget = other.GetComponent<IBattle>();     // 콜라이더에 닿으면 바로 공격발동
            ChangeState(MonsterState.Attack);
            return;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ChangeState(MonsterState.Chase);
            return;
        }

    }

    /// <summary>
    /// 상태변화함수
    /// </summary>
    /// <param name="newState">새로운상태표시</param>
    public void ChangeState(MonsterState newState)
    {
        if (isDead)
        {
            return;
        }

        switch (state) //이전 상태를 나가면서 해야할일
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
        switch (newState) // 새로운 상태로 들어가면서 해야할일
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
        state = newState; //새로운 상태로 변경
        anim.SetInteger("MonsterState", (int)state); // 새로운상태에 따라 애니메이션 변경
    }

    /// <summary>
    /// 죽었을때 사용되는 함수
    /// </summary>
    public void DiePresent()
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

    /// <summary>
    /// 인터페이스 IBattle의 공격하기위한 함수
    /// </summary>
    /// <param name="target">공격하기 위한 타겟</param>
    public void Attack(IBattle target)
    {
        if (target != null)
        {
            float damage = AttackPower;
            target.TakeDamage(damage);
        }
    }
    /// <summary>
    /// 인터페이스 IBattle의 공격받기 위한 함수
    /// </summary>
    /// <param name="damage">받을 데미지</param>
    public void TakeDamage(float damage,int type=0)
    {
        float finalDamage = damage - defencePower;
        if (finalDamage < 1.0f)
        {
            finalDamage = 1.0f;
        }
        HP -= finalDamage;

        if (HP > 0.0f)
        {
            if (type == 1)
            {
                anim.SetTrigger("TakeDamage");
                attackCoolTime = attackSpeed;
            }
        }
        else
        {
            Die();
        }
        //Debug.Log($"MonsterHP : {hp}");
    }

    /// <summary>
    /// 죽었을때 사용되는 함수
    /// </summary>
    public void Die()
    {
        if (isDead == false)
        {
            GameManager.INSTANCE.MONSTERLIVECOUNT--;
            ChangeState(MonsterState.Dead);
        }
    }

    /// <summary>
    /// 죽을때 사용되는 함수
    /// </summary>
    /// <returns>1.0f뒤 실행됨</returns>
    IEnumerator DeadEffect()
    {
        yield return new WaitForSeconds(1.0f);
        float r = Random.Range(0f, 1.0f);
        if(r<0.1f)
        {
            ItemFactory.MakeItem(ItemIDCode.Trap, new(transform.position.x, 1.0f, transform.position.z), true);
        }
        else if(r<0.2f)
        {
            ItemFactory.MakeItem(ItemIDCode.HP_potion, new(transform.position.x, 1.0f, transform.position.z), true);
        }


        Collider[] colliders = GetComponents<Collider>();
        foreach (var col in colliders)
        {
            col.enabled = false;
        }
        nav.enabled = false;
        gameObject.SetActive(false);
        //Destroy(this.gameObject, 1.0f);
    }
    /// <summary>
    /// 인터페이스 IHealth의 회복할때 사용할 함수
    /// </summary>
    /// <param name="heal">회복될 양</param>
    public void TakeHeal(float heal)
    {
        Debug.Log("몬스터 힐링");
    }
}
