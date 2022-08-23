using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Wolf_Weapon : MonoBehaviour
{
    PlayerWolf weaponOwner;

    private void Awake()
    {
        weaponOwner = GameObject.FindObjectOfType<PlayerWolf>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IBattle battle = other.GetComponent<IBattle>();
            if (battle != null)
            {
                battle.TakeDamage(50.0f);
                if(weaponOwner.isSkillON == true)
                {
                    StartCoroutine(SkillAttack(other)); //넉백 공격하려면 kinematic을 풀어야하는데 넉백될동안만 풀기위해 코루틴 사용
                }
                
            }

        }
    }

    IEnumerator SkillAttack(Collider other)
    {
        other.attachedRigidbody.isKinematic=false;
        Debug.Log("스킬중 공격 발동");
        other.attachedRigidbody.AddForce(-other.transform.forward * 5.0f, ForceMode.Impulse);
        yield return new WaitForSeconds(1.0f);
        if (other.attachedRigidbody != null)
        {
            other.attachedRigidbody.isKinematic = true;
        }
    }

}
