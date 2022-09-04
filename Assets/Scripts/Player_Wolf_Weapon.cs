using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Wolf_Weapon : MonoBehaviour
{
    //플레이어의 무기에 들어가는 스크립트


    PlayerWolf weaponOwner;
    public GameObject attackEffect1;
    public GameObject attackEffect2;
    private void Start()
    {
        weaponOwner = GameManager.INSTANCE.PLAYER.GetComponent<PlayerWolf>();
        //attackEffect1 = GameObject.Find("HitEffect");
        //attackEffect2 = GameObject.Find("HitSkillEffect");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IBattle battle = other.GetComponent<IBattle>();
            if (battle != null)
            {
                battle.TakeDamage(50.0f,1);
                if(weaponOwner.isSkillOn)
                {
                    StartCoroutine(SkillAttack(other));
                    Instantiate(attackEffect2, other.transform.position, Quaternion.identity);
                }else
                {
                    Instantiate(attackEffect1, other.transform.position, Quaternion.identity);
                }
            }

        }
    }
    /// <summary>
    /// 스킬발동시 적들을 밀어내는 IEnumerator
    /// </summary>
    /// <param name="other">타겟</param>
    /// <returns></returns>
    IEnumerator SkillAttack(Collider other)
    {
        other.attachedRigidbody.isKinematic = false;
        Debug.Log("스킬중 공격 발동");
        other.attachedRigidbody.AddForce(-other.transform.forward * 5.0f, ForceMode.Impulse);
        yield return new WaitForSeconds(1.0f);
        if (other.attachedRigidbody != null)
        {
            other.attachedRigidbody.isKinematic = true;
        }
    }
}
