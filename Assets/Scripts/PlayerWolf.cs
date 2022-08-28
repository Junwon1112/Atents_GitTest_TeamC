using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerWolf : MonoBehaviour, IHealth, IEquipTarget
{
    Rigidbody rigid = null;
    Vector3 inputDir = Vector3.zero;
    public float turnSpeed = 10.0f;

    public float forwardJumpPower = 3.0f;
    public float upJumpPower = 10.0f;
    public int jumpTime = 2;
    public float skillContinueTime = 10.0f;
    int tempJumpTime;


    public float moveSpeed = 3.0f;
    Quaternion targetRotation = Quaternion.identity;

    Animator anim = null;
    ParticleSystem SkillAura;


    public float Player_Hp = 100.0f;
    float Player_MaxHp = 100.0f;

    //float inputRotY;
    //float inputRotX;

    // ����----------------
    public bool isDelay;
    public float delayTime = 5.0f;


    // �κ��丮�� --------------
    Inventory inven;
    ItemSlot equipItemSlot;
    public ItemSlot EquipItemSlot => equipItemSlot;

    // ������ �� -----------------------
    int money = 0;  // �÷��̾� ��
    public int Money
    {
        get => money;
        set
        {
            if (money != value)
            {
                money = value;
                OnMoneyChange?.Invoke(money);
            }
        }
    }
    public System.Action<int> OnMoneyChange; // ���� ����Ǹ� ����� ��������Ʈ
    float itemPickupRange = 2.0f;
    float dropRange = 2.0f;
    public void ItemPickup()
    {
        // �ֺ��� Item���̾ �ִ� �ö��̴� ���� ��������
        Collider[] cols = Physics.OverlapSphere(transform.position, itemPickupRange, LayerMask.GetMask("Item"));
        foreach (var col in cols)
        {
            Item item = col.GetComponent<Item>();

            // as : as ���� ������ as ���� Ÿ������ ĳ������ �Ǹ� ĳ���� �� ����� �ְ� �ȵǸ� null�� �ش�.
            // is : is ���� ������ is ���� Ÿ������ ĳ������ �Ǹ� true, �ƴϸ� false
            IConsumable consumable = item.data as IConsumable;
            if (consumable != null)
            {
                consumable.Consume(this);   // ���ڸ��� �Һ��ϴ� ������ �������� ������ ȿ���� �°� ����                
                Destroy(col.gameObject);
            }
            else
            {
                if (inven.AddItem(item.data))
                {
                    Destroy(col.gameObject);
                }
            }
        }

        //Debug.Log($"�÷��̾��� �� : {money}");
    }

    public Vector3 ItemDropPosition(Vector3 inputPos)
    {
        Vector3 result = Vector3.zero;
        Vector3 toInputPos = inputPos - transform.position;
        if (toInputPos.sqrMagnitude > dropRange * dropRange)
        {
            // inputPos�� dropRange �ۿ� �ִ�.
            result = transform.position + toInputPos.normalized * dropRange;
        }
        else
        {
            // inputPos�� dropRange �ȿ� �ִ�.
            result = inputPos;
        }

        return result;
    }



    public float PlayerHp
    {
        get => Player_Hp;
        set => Player_Hp = value;
    }

    private void Awake()
    {
        SkillAura = GetComponentInChildren<ParticleSystem>();
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        inven = new Inventory();
    }

    private void Start()
    {
        tempJumpTime = jumpTime;
        //SkillAura.Stop();
        GameManager.INSTANCE.InvenUI.InitializeInventory(inven);
    }

    private void FixedUpdate()
    {
        if (!GameManager.INSTANCE.CAMERASWAP)
        {
            Keyboard k = Keyboard.current;
            //anim.SetBool("isMove", true);
            rigid.MovePosition(rigid.position + moveSpeed * Time.fixedDeltaTime * inputDir);
            //rigid.MoveRotation(Quaternion.Lerp(rigid.rotation, Quaternion.Euler(0, inputRot ,0), 0.5f));
            //rigid.MovePosition(rigid.position + moveSpeed * Time.deltaTime * inputDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
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
            if (jumpTime > 0 && context.started)
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
        yield return new WaitForSeconds(skillContinueTime);
        SkillAura.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpTime = tempJumpTime;
        }
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

    public float MAXHP
    {
        get
        {
            return Player_MaxHp;
        }
    }

    public Action onHealthChange { get; set; }

    public void TakeDamage(float damage)
    {
        float finalDamage = damage;
        if(finalDamage<1.0f)
        {
            finalDamage = 1.0f;
        }
        HP -= finalDamage;
        if(HP<0.1f)
        {
            Die();
        }else
        {
            Hit();
        }
        
    }

    public void TakeHeal(float heal)
    {
        HP += heal;
        if (HP > 100.0f)
        {
            HP = 100.0f;
        }
    }

    public void Hit()
    {
        anim.SetTrigger("hit");
    }
    void Die()
    {
        anim.SetTrigger("Die");
    }
    public void Test()
    {
        inven.AddItem(ItemIDCode.HP_potion);
    }

    public void EquipWeapon(ItemSlot weaponSlot)
    {
        //ShowWeapons(true);  // ����ϸ� ������ ���̵���
        //GameObject obj = Instantiate(weaponSlot.SlotItemData.prefab, weapon.transform);  // ���� ����� ������ �����ϱ�
        //obj.transform.localPosition = new(0, 0, 0);             // �θ𿡰� ��Ȯ�� �ٵ��� ������ 0,0,0���� ����
        equipItemSlot = weaponSlot;                             // ����� ������ ǥ��
        equipItemSlot.ItemEquiped = true;
    }

    public void UnEquipWeapon()
    {
        equipItemSlot.ItemEquiped = false;
        equipItemSlot = null;   // ��� �����ٴ� ���� ǥ���ϱ� ����(IsWeaponEquiped �����)
        //Transform weaponChild = weapon.transform.GetChild(0);
        //weaponChild.parent = null;          // ���Ⱑ �ٴ� ��ҿ� �ִ� �ڽ� �����
        //Destroy(weaponChild.gameObject);    // ���� ��Ʈ����
    }
}
