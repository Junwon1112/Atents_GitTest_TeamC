using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public ParticleSystem trapIsHere;
    public GameObject trapEffect;
    //float trapDamage = 50.0f;
    //Monster monster;

    private void Start()
    {
        trapIsHere = GetComponentInChildren<ParticleSystem>();
        trapEffect.SetActive(false);
        //monster = GetComponent<Monster>();
        gameObject.transform.parent = null;
        trapIsHere.Play();
        //trapEffect.SetActive(false);
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //particles[1].;
            trapEffect.gameObject.SetActive(true);
            trapEffect.gameObject.GetComponent<ParticleSystem>().Play();
            trapIsHere.Stop();
            //DeleteChilds();
            Destroy(gameObject, 0.3f);
            collision.GetComponent<Monster>().Die();
        }

    }
    //void DeleteChilds()
    //{
    //    Transform[] childList = this.GetComponentsInChildren<Transform>();
    //    if(childList != null)
    //    {
    //        for(int i = 1; i<childList.Length; i++)
    //        {
    //            if(childList[i] != transform)
    //            {
    //                Destroy(childList[i].gameObject);
    //            }
    //        }
    //    }
    //}
}
