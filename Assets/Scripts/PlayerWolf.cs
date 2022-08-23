using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerWolf : MonoBehaviour , IHealth ,IBattle
{
    //플레이어에 들어가는 스크립트
    Player_Wolf actions;


    Rigidbody rigid = null;
    Vector3 inputDir = Vector3.zero;
    public float turnSpeed = 10.0f;

    public float forwardJumpPower = 3.0f;
    public float upJumpPower = 10.0f;
    public int jumpTime = 2;
    public float skillContinueTime = 10.0f;
    int tempJumpTime;
    bool isDead = false;

    public float skillCooltime; // 쿨타임 13초로 설정예정


    public float moveSpeed = 3.0f;

    Animator anim = null;
    ParticleSystem SkillAura;
    public bool isSkillOn = false;

    bool isAttackOn;
    bool isSkillMotionOn;

    int money = 0;
    float rx;
    float ry;

    //미니맵관련ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    private Vector3 quadPosition;
    Transform quad;


    //IHealthㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    float Player_Hp = 100.0f;
    float Player_MaxHp = 100.0f;

    //IBattleㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    float attackPower = 10.0f;
    float defencePower = 1.0f;

    //임시로 쓰는것 ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    PlayerPotion PP; //플레이어 포션 찾아두기


    private void Awake()
    {
        actions = new();
        SkillAura = GetComponentInChildren<ParticleSystem>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        PP=FindObjectOfType<PlayerPotion>();
        quad = transform.Find("Player_WereWolf_Quad");
        

    }

    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += OnmoveInput;
        actions.Player.Move.canceled += OnmoveInput;
        actions.Player.Attack.performed += OnAttackInput;
        actions.Player.Jump.performed += OnJumpInput;
        actions.Player.Skill.performed += OnSkillInput;
        actions.Player.UseScroll.performed += OnUseScroll;
        actions.Player.UsePotion.performed += OnUsePotion;
        actions.Player.Look.performed += OnLook;
    }

    private void OnDisable()
    {
        actions.Player.Look.performed -= OnLook;
        actions.Player.UsePotion.performed -= OnUsePotion;
        actions.Player.UseScroll.performed -= OnUseScroll;
        actions.Player.Skill.performed -= OnSkillInput;
        actions.Player.Jump.performed -= OnJumpInput;
        actions.Player.Attack.performed -= OnAttackInput;
        actions.Player.Move.canceled -= OnmoveInput;
        actions.Player.Move.performed -= OnmoveInput;
        actions.Player.Disable();
    }

   
    private void Start()
    {
        tempJumpTime = jumpTime;
        MONEY += 500;
        //SkillAura.Stop();
    }

    private void FixedUpdate()
    {
        skillCooltime -= Time.fixedDeltaTime;
        // GameManager에 있는 CAMERASWAP변수를 통해 타워설치인지 전투상태인지 확인 전투상태일때만 조작가능
        if (!GameManager.INSTANCE.CAMERASWAP)  
        {
            transform.Translate(moveSpeed * Time.fixedDeltaTime * inputDir, Space.Self);
            

            //플레이어가 이동중일때 애니메이션을 재생
            if (inputDir.x != 0 || inputDir.z != 0)
            {
                anim.SetBool("isMove", true);
            }
            else
            {
                anim.SetBool("isMove", false);
            }
            
            

        }

        //플레이어의 미니맵표시가 돌아가지 않게 고정
        quadPosition = new Vector3(quad.position.x, transform.position.y, quad.position.z);
        quad.transform.LookAt(quadPosition);

    }

    //플레이어가 마우스 방향을 바라보게하는 함수
    private void OnLook(InputAction.CallbackContext obj)
    {
        if (isDead == false && !GameManager.INSTANCE.CAMERASWAP)
        {

            float mx = obj.ReadValue<Vector2>().x;
            float my = obj.ReadValue<Vector2>().y;

            //rx += rotSpeed * my * Time.deltaTime;
            ry += turnSpeed * mx * Time.deltaTime;

            rx = Mathf.Clamp(rx, -80, 50);

            transform.eulerAngles = new Vector3(0, ry, 0);

        }
    }

    //플레이어 이동함수
    public void OnmoveInput(InputAction.CallbackContext context)
    {
        if (!isAttackOn && !isSkillMotionOn) //공격과 스킬사용중 움직임 멈춤
        {
            Vector3 input;
            input = context.ReadValue<Vector2>();
            inputDir.x = input.x;
            inputDir.y = 0.0f;
            inputDir.z = input.y;
        }
        


    }
    //플레이어 공격함수
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        
        if (!GameManager.INSTANCE.CAMERASWAP)
        {
            if (transform.position.y < 1.3f)
            {
                inputDir = Vector3.zero; //움직임 도중 공격할 경우 정지시킴
                anim.SetFloat("ComboState", Mathf.Repeat(anim.GetCurrentAnimatorStateInfo(0).normalizedTime, 1.0f));
                anim.ResetTrigger("isAttack");
                anim.SetTrigger("isAttack");
                anim.SetBool("isAttackM", true);
            }
        }
    }

    //플레이어 점프함수
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (!GameManager.INSTANCE.CAMERASWAP)
        {
            if (jumpTime > 0 && !isAttackOn && !isSkillMotionOn)
            {
                anim.ResetTrigger("isJump");
                anim.SetTrigger("isJump");

                rigid.AddForce(transform.up * upJumpPower + transform.forward * forwardJumpPower, ForceMode.Impulse);
                jumpTime--;
            }
        }

    }

    //플레이어 스킬사용함수
    public void OnSkillInput(InputAction.CallbackContext context)
    {
        if (!GameManager.INSTANCE.CAMERASWAP)
        {
            if (skillCooltime < 0 && (transform.position.y < 1.3f))
            {
                inputDir = Vector3.zero; //이동중 스킬사용시 정지
                skillCooltime = 13; //스킬 쿨타임 13초
                StartCoroutine(SkillAuraOnOff());
                anim.SetBool("isSkill", true);
            }
            else
            {
                Debug.Log($"스킬 쿨타임이 {skillCooltime}초 남았습니다");
            }
        }

    }
    IEnumerator SkillAuraOnOff()
    {
        //gameObject.GetComponentInChildren<ParticleSystem>().
        SkillAura.Play();
        isSkillOn = true;
        yield return new WaitForSeconds(skillContinueTime);
        isSkillOn = false;
        SkillAura.Stop();
    }

    //땅에 닿았을때 점프횟수 초기화 해주는 콜라이더 함수
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpTime = tempJumpTime;
        }
    }

    //스크롤 사용함수
    private void OnUseScroll(InputAction.CallbackContext obj)
    {
        Debug.Log("스크롤 사용");
    }

    //포션 사용함수
    private void OnUsePotion(InputAction.CallbackContext obj)
    {
        PP.OnDrinkPotion();
    }

    //IHealth 인터페이스 구현
    public float HP
    {
        get
        {
            return Player_Hp;
        }
        set
        {
            Player_Hp = Mathf.Clamp(value, 0, Player_MaxHp);
            onHealthChange?.Invoke(); //델리게이트를 만들어서 HP가 변화했을때만 hp바가 움직이겠끔 구현

        }

    }

    public float MaxHP
    {
        get
        {
            return Player_MaxHp;
        }
    }

    public Action onHealthChange { get; set; }

    public void TakeHeal(float heal)
    {
        if (isDead == false)
        {
            HP += heal;
            if (HP > 100.0f)
            {
                HP = 100.0f;
            }
        }
    }

    //IBattle 인터페이스 구현

    public float AttackPower
    {
        get => attackPower;
    }

    public float DefencePower
    {
        get => defencePower;
    }

    public void TakeDamage(float damage)
    {
        if (isDead == false)
        {
            float finalDamage = damage;
            if (finalDamage < 1.0f)
            {
                finalDamage = 1.0f;
            }
            HP -= finalDamage;
            if (HP < 0.1f)
            {
                Die();
            }
            else
            {
                Hit();
            }
        }
        
    }

    public void Hit()
    {
        anim.SetTrigger("hit");
    }
    void Die()
    {
        if (isDead == false)
        {
            anim.SetTrigger("Die");
            HP = 0.0f;
            actions.Disable();
            isDead = true;
        }

    }

    public void Attack(IBattle target)
    {
        
    }

   
    //Moneyㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    public int MONEY
    {
        get { return money; }
        set { money = value;
            MoneyChange?.Invoke();
        }
    }

    public Action MoneyChange;


    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ

    public void IsAttackOn()
    {
        isAttackOn = true;
        //Debug.Log("공격실행");
    }

    public void IsAttackOff()
    {
        isAttackOn = false;
        //Debug.Log("공격중지");
    }

    public void IsSkillMotionOn()
    {
        isSkillMotionOn = true;
    }

    public void IsSkillMotionOff()
    {
        isSkillMotionOn = false;
    }

}
