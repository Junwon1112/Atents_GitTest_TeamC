using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerWolf : MonoBehaviour , IHealth ,IBattle
{
    //�÷��̾ ���� ��ũ��Ʈ
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

    public float skillCooltime; // ��Ÿ�� 13�ʷ� ��������


    public float moveSpeed = 3.0f;

    Animator anim = null;
    ParticleSystem SkillAura;
    public bool isSkillOn = false;

    bool isAttackOn;
    bool isSkillMotionOn;

    int money = 0;
    float rx;
    float ry;

    //�̴ϸʰ��äѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�
    private Vector3 quadPosition;
    Transform quad;


    //IHealth�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�

    float Player_Hp = 100.0f;
    float Player_MaxHp = 100.0f;

    //IBattle�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�

    float attackPower = 10.0f;
    float defencePower = 1.0f;

    //�ӽ÷� ���°� �ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�
    PlayerPotion PP; //�÷��̾� ���� ã�Ƶα�


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
        // GameManager�� �ִ� CAMERASWAP������ ���� Ÿ����ġ���� ������������ Ȯ�� ���������϶��� ���۰���
        if (!GameManager.INSTANCE.CAMERASWAP)  
        {
            transform.Translate(moveSpeed * Time.fixedDeltaTime * inputDir, Space.Self);
            

            //�÷��̾ �̵����϶� �ִϸ��̼��� ���
            if (inputDir.x != 0 || inputDir.z != 0)
            {
                anim.SetBool("isMove", true);
            }
            else
            {
                anim.SetBool("isMove", false);
            }
            
            

        }

        //�÷��̾��� �̴ϸ�ǥ�ð� ���ư��� �ʰ� ����
        quadPosition = new Vector3(quad.position.x, transform.position.y, quad.position.z);
        quad.transform.LookAt(quadPosition);

    }

    //�÷��̾ ���콺 ������ �ٶ󺸰��ϴ� �Լ�
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

    //�÷��̾� �̵��Լ�
    public void OnmoveInput(InputAction.CallbackContext context)
    {
        if (!isAttackOn && !isSkillMotionOn) //���ݰ� ��ų����� ������ ����
        {
            Vector3 input;
            input = context.ReadValue<Vector2>();
            inputDir.x = input.x;
            inputDir.y = 0.0f;
            inputDir.z = input.y;
        }
        


    }
    //�÷��̾� �����Լ�
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        
        if (!GameManager.INSTANCE.CAMERASWAP)
        {
            if (transform.position.y < 1.3f)
            {
                inputDir = Vector3.zero; //������ ���� ������ ��� ������Ŵ
                anim.SetFloat("ComboState", Mathf.Repeat(anim.GetCurrentAnimatorStateInfo(0).normalizedTime, 1.0f));
                anim.ResetTrigger("isAttack");
                anim.SetTrigger("isAttack");
                anim.SetBool("isAttackM", true);
            }
        }
    }

    //�÷��̾� �����Լ�
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

    //�÷��̾� ��ų����Լ�
    public void OnSkillInput(InputAction.CallbackContext context)
    {
        if (!GameManager.INSTANCE.CAMERASWAP)
        {
            if (skillCooltime < 0 && (transform.position.y < 1.3f))
            {
                inputDir = Vector3.zero; //�̵��� ��ų���� ����
                skillCooltime = 13; //��ų ��Ÿ�� 13��
                StartCoroutine(SkillAuraOnOff());
                anim.SetBool("isSkill", true);
            }
            else
            {
                Debug.Log($"��ų ��Ÿ���� {skillCooltime}�� ���ҽ��ϴ�");
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

    //���� ������� ����Ƚ�� �ʱ�ȭ ���ִ� �ݶ��̴� �Լ�
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpTime = tempJumpTime;
        }
    }

    //��ũ�� ����Լ�
    private void OnUseScroll(InputAction.CallbackContext obj)
    {
        Debug.Log("��ũ�� ���");
    }

    //���� ����Լ�
    private void OnUsePotion(InputAction.CallbackContext obj)
    {
        PP.OnDrinkPotion();
    }

    //IHealth �������̽� ����
    public float HP
    {
        get
        {
            return Player_Hp;
        }
        set
        {
            Player_Hp = Mathf.Clamp(value, 0, Player_MaxHp);
            onHealthChange?.Invoke(); //��������Ʈ�� ���� HP�� ��ȭ�������� hp�ٰ� �����̰ڲ� ����

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

    //IBattle �������̽� ����

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

   
    //Money�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�
    public int MONEY
    {
        get { return money; }
        set { money = value;
            MoneyChange?.Invoke();
        }
    }

    public Action MoneyChange;


    //�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�

    public void IsAttackOn()
    {
        isAttackOn = true;
        //Debug.Log("���ݽ���");
    }

    public void IsAttackOff()
    {
        isAttackOn = false;
        //Debug.Log("��������");
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
