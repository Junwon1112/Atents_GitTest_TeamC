using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Wolf_Weapon : MonoBehaviour
{
    PlayerWolf weaponOwner;

    private void Start()
    {
        weaponOwner = GameManager.INSTANCE.PLAYER.GetComponent<PlayerWolf>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IBattle battle = other.GetComponent<IBattle>();
            if (battle != null)
            {
                battle.TakeDamage(50.0f);
                if(weaponOwner.isSkillOn)
                {
                    StartCoroutine(SkillAttack(other));
                }
            }

        }
    }

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
