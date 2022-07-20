using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_EnemySpawer : MonoBehaviour
{
    public GameObject Enemy = null;

    private float SpawerDelayMax = 5.0f;
    private float SpawerDelay = 0.0f;


    private void Start()
    {
        //StartCoroutine(Spawer());
    }

    private void Update()
    {
        if (!GameManager.INSTANCE.CAMERASWAP)
        {
            if (SpawerDelay < SpawerDelayMax)
            {
                SpawerDelay += Time.deltaTime;
            }

            if (SpawerDelay > SpawerDelayMax)
            {
                SpawerDelay = 0.0f;
                GameObject e = Instantiate(Enemy);
                e.transform.position = transform.position;
            }
        }
    }
    IEnumerator Spawer()
    {
        while(true)
        {
            if (!GameManager.INSTANCE.CAMERASWAP)
            {
                yield return new WaitForSeconds(10.0f);
                GameObject e = Instantiate(Enemy);
                e.transform.position = transform.position;
            }
        }
    }

}
