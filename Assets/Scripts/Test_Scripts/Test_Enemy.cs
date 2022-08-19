using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Test_Enemy : MonoBehaviour
{
    public Transform target;
    GameObject player;

    NavMeshAgent nav=null;


    Rigidbody rigid;

    // Start is called before the first frame update
    private void Awake()
    {
        
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nav =GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.INSTANCE.CAMERASWAP)
        {
           
            target = player.transform;
            nav.SetDestination(target.position);
        }
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    void FreezeVelocity()
    {
        rigid.velocity = Vector3.zero;
    }
}
