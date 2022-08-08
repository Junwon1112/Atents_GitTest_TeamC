using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerWolf : MonoBehaviour , IHealth ,IBattle
{
    Player_Wolf actions;
    InputAction test;

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
    public bool isSkillON = false;
    public float TurnSpeed = 0.1f;

    GameObject obj;


    //float inputRotY;
    //float inputRotX;

    //public Camera PlayerCamera;

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
        obj = GetComponent<GameObject>();
 
    }

    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += OnmoveInput;
        actions.Player.Move.canceled += OnmoveInput;
        actions.Player.Attack.performed += OnAttackInput;
        actions.Player.Jump.performed += OnJumpInput;
        actions.Player.Skill.performed += OnSkillInput;
        actions.Player.Screen.performed += OnScreenInput;
        actions.Player.UseScroll.performed += OnUseScroll;
        actions.Player.UsePotion.performed += OnUsePotion;
        actions.Player.UseItem.performed += OnUseItem;
    }

    

    private void OnDisable()
    {
        actions.Player.UseItem.performed -= OnUseItem;
        actions.Player.UsePotion.performed -= OnUsePotion;
        actions.Player.UseScroll.performed -= OnUseScroll;
        actions.Player.Skill.performed -= OnSkillInput;
        actions.Player.Jump.performed -= OnJumpInput;
        actions.Player.Screen.performed -= OnScreenInput;
        actions.Player.Attack.performed -= OnAttackInput;
        actions.Player.Move.canceled -= OnmoveInput;
        actions.Player.Move.performed -= OnmoveInput;
        actions.Player.Disable();
        Debug.Log("활성");
    }

   
    private void Start()
    {
        tempJumpTime = jumpTime;

        //디버깅용
        HP = 0;
        //isDead = true;
        //SkillAura.Stop();
    }

    private void FixedUpdate()
    {
        
        if (!GameManager.INSTANCE.CAMERASWAP)
        {
            Keyboard k = Keyboard.current;
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
            Vector3 mousePos = Mouse.current.position.ReadValue();
            //Debug.Log($"{mousePos}"); //마우스 좌표 : x,y값 받아옴, z는 0 : 고정된 값
            //Ray cameraRay = PlayerCamera.ScreenPointToRay(mousePos);
            Ray cameraRay = Camera.main.ScreenPointToRay(mousePos);

            //Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);
            Plane GroupPlane = new Plane(transform.forward, -10000);


            float rayLength;
            if (GroupPlane.Raycast(cameraRay, out rayLength))
            {
                Vector3 pointTolook = cameraRay.GetPoint(rayLength);
                //Debug.Log($"{pointTolook}"); // 레이를 이용해 xz값으로 바꿈, y는 0 : 마우스를 멈춰도 변화하는 값

                Vector3 LookDir = (pointTolook - transform.position).normalized;

                LookDir.y = 0.0f;
                //LookDir.x = Mathf.Clamp(LookDir.x, -80.0f, 80.0f);

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(LookDir), Time.fixedDeltaTime * TurnSpeed);

                //transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));

            }



        }



    }

    public void OnmoveInput(InputAction.CallbackContext context)
    {
        //if(isDead == false)
        //{
            Vector3 input;
            input = context.ReadValue<Vector2>();
            inputDir.x = input.x;
            inputDir.y = 0.0f;
            inputDir.z = input.y;
            if (inputDir.sqrMagnitude > 0.0f)    //sqrMagnitude => vector를 제곱한거, root 연산만안한것
            {

                //inputDir = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * inputDir;
                // 카메라의 y축 회전만 따로 분리해서 회전
                //targetRotation = Quaternion.LookRotation(inputDir);
                //카메라 보는방향기준으로 입력을 바꿈

            }
    }

    private void OnScreenInput(InputAction.CallbackContext context)
    {
        //Vector2 mousePos = context.ReadValue<Vector2>();

        ////Ray cameraRay = PlayerCamera.ScreenPointToRay(mousePos);
        //Ray cameraRay = Camera.main.ScreenPointToRay(mousePos);

        ////Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);
        //Plane GroupPlane = new Plane(transform.forward, -10000);


        //float rayLength;
        //if (GroupPlane.Raycast(cameraRay, out rayLength))
        //{
        //    Vector3 pointTolook = cameraRay.GetPoint(rayLength);
        //    //Debug.Log($"{pointTolook}"); // 레이를 이용해 xz값으로 바꿈, y는 0 : 마우스를 멈춰도 변화하는 값

        //    Vector3 LookDir = (pointTolook - transform.position).normalized;

        //    LookDir.y = 0.0f;
        //    //LookDir.x = Mathf.Clamp(LookDir.x, -80.0f, 80.0f);

        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(LookDir), Time.fixedDeltaTime * TurnSpeed);

        //    //transform.LookAt(new Vector3(pointTolook.x, transform.position.y, pointTolook.z));

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
            if (jumpTime > 0 && context.performed)
            {
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
        isSkillON = true;
        yield return new WaitForSeconds(skillContinueTime);
        isSkillON = false;
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
        Debug.Log("스크롤 사용");
        //1번키
    }

    private void OnUsePotion(InputAction.CallbackContext obj)
    {
        PP.OnDrinkPotion();
        //2번키
    }

    private void OnUseItem(InputAction.CallbackContext obj)
    {
        // 예비 아이템, 3번키
    }


    // HPㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
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
        if(isDead == false)
        {
            anim.SetTrigger("Die");
            anim.SetBool("isDie", true);
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
        if(isDead == false)
        {
            HP += heal;
            if (HP > 100.0f)
            {
                HP = 100.0f;
            }
        }
    }
}
