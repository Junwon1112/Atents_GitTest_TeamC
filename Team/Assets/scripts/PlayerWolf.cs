using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerWolf : MonoBehaviour , IHealth ,IBattle
{
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


    public float moveSpeed = 3.0f;
    Quaternion targetRotation = Quaternion.identity;

    Animator anim = null;
    ParticleSystem SkillAura;
    public float TurnSpeed = 0.1f;
    public bool isSkillOn = false;

    private Vector3 quadPosition;
    Transform quad;

    int money = 0;
    //float inputRotY;
    //float inputRotX;

    //public Camera PlayerCamera;

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
        
        if (!GameManager.INSTANCE.CAMERASWAP)
        {
            //anim.SetBool("isMove", true);
            transform.Translate(moveSpeed * Time.fixedDeltaTime * inputDir, Space.Self);
            //rigid.MovePosition(rigid.position + moveSpeed * Time.fixedDeltaTime * inputDir);
            //rigid.MoveRotation(Quaternion.Lerp(rigid.rotation, Quaternion.Euler(0, inputRot ,0), 0.5f));
            //rigid.MovePosition(rigid.position + moveSpeed * Time.deltaTime * inputDir);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            //transform.LookAt(inputDir);


            if (inputDir.x != 0 || inputDir.z != 0)
            {
                anim.SetBool("isMove", true);
            }
            else
            {
                anim.SetBool("isMove", false);
            }           
        }
        quadPosition = new Vector3(quad.position.x, transform.position.y, quad.position.z);
        quad.transform.LookAt(quadPosition);
    }

    private void OnLook(InputAction.CallbackContext obj)
    {
        if (isDead == false)
        {
            Vector3 mousePos = Mouse.current.position.ReadValue(); 
            Ray cameraRay = Camera.main.ScreenPointToRay(mousePos);

            Plane GroupPlane = new Plane(transform.forward, -10000);


            float rayLength;
            if (GroupPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointTolook = cameraRay.GetPoint(rayLength);
                //Debug.Log($"{pointTolook}"); // ���̸� �̿��� xz������ �ٲ�, y�� 0 : ���콺�� ���絵 ��ȭ�ϴ� ��

                Vector3 LookDir = (pointTolook - transform.position).normalized;

                LookDir.y = 0.0f;
                //LookDir.x = Mathf.Clamp(LookDir.x, -1.0f, 1.0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(LookDir), Time.deltaTime * TurnSpeed);
                
                //transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));

            }
        }        
    }


    public void OnmoveInput(InputAction.CallbackContext context)
    {
        Vector3 input;
        input = context.ReadValue<Vector2>();
        inputDir.x = input.x;
        inputDir.y = 0.0f;
        inputDir.z = input.y;
        if (inputDir.sqrMagnitude > 0.0f)    //sqrMagnitude => vector�� �����Ѱ�, root ���길���Ѱ�
        {
            //inputDir = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * inputDir;
            // ī�޶��� y�� ȸ���� ���� �и��ؼ� ȸ��
            //targetRotation = Quaternion.LookRotation(inputDir);
            //ī�޶� ���¹���������� �Է��� �ٲ�

        }





        //inputRot = - Mathf.Acos(inputDir.x) * 180 / Mathf.PI - Mathf.Asin(inputDir.y) * 180 / Mathf.PI;


        //if (inputDir.y == 1) 
        //{
        //    inputRot = 0;
        //}
        //else if(inputDir.y == -1)
        //{
        //    inputRot = 180;
        //}
        //if (inputDir.x == 1)
        //{
        //    inputRot = 90;
        //}
        //else if (inputDir.x == -1)
        //{
        //    inputRot = -90;
        //}

        //inputRot = inputRotX + inputRotY;


        //if (context.canceled)
        //{
        //    inputRot = 0;
        //}

    }

    public void OnAttackInput(InputAction.CallbackContext context)
    {
        /*if (!GameManager.INSTANCE.CAMERASWAP)
        {
            if (context.performed)
            {
                anim.SetBool("isAttack", true);
            }
            else if (context.canceled)
            {
                anim.SetBool("isAttack", false);
            }
        }*/
        if (!GameManager.INSTANCE.CAMERASWAP)
        {
            anim.SetFloat("ComboState", Mathf.Repeat(anim.GetCurrentAnimatorStateInfo(0).normalizedTime, 1.0f));
            anim.ResetTrigger("isAttack");
            anim.SetTrigger("isAttack");
            anim.SetBool("isAttackM", true);
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (!GameManager.INSTANCE.CAMERASWAP)
        {
            //Debug.Log("����1");
            if (jumpTime > 0)
            {
                //Debug.Log("����2");
                anim.ResetTrigger("isJump");
                anim.SetTrigger("isJump");

                rigid.AddForce(transform.up * upJumpPower + transform.forward * forwardJumpPower, ForceMode.Impulse);
                jumpTime--;
            }
        }

    }

    public void OnSkillInput(InputAction.CallbackContext context)
    {
        if (!GameManager.INSTANCE.CAMERASWAP)
        {
            StartCoroutine(SkillAuraOnOff());
            anim.SetBool("isSkill", true);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpTime = tempJumpTime;
        }
    }

    private void OnUseScroll(InputAction.CallbackContext obj)
    {
        Debug.Log("��ũ�� ���");
    }

    private void OnUsePotion(InputAction.CallbackContext obj)
    {
        PP.OnDrinkPotion();
    }

    // HP�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�
    public float HP
    {
        get
        {
            return Player_Hp;
        }
        set
        {
            Player_Hp = Mathf.Clamp(value, 0, Player_MaxHp);
            
            
            onHealthChange?.Invoke();

            
            //Debug.Log(Player_Hp);
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

    //IBattle�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�

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
}
