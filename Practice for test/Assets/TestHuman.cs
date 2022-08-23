using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestHuman : MonoBehaviour
{
    Transform destination;
    Vector3 targetVector;
    NavMeshAgent agent;
    //bool isArrive = false;
    int num = 0;
    int state = 0;
    float findRange = 5.0f;
    Vector3 targetPosition = new();
    float waitTime;

    private void Awake()
    {
        //agent = GetComponent<NavMeshAgent>();
        agent = GameObject.FindObjectOfType<NavMeshAgent>();
        destination = GameObject.Find("Destination").GetComponent<Transform>();
        
    }

    private void Start()
    {
        
    }



    private void Update()
    {
        SetState();
        //SetPatrols();
        switch (state)
        {
            case 0: //patrol
                SetPatrols();
                break;
            case 1:
                Chase();
                break;
            case 2:
                Attack();
                break;
           

        }

    }

    

    int SetState()
    {
        if(FindPlayer() == false)
        {
            
            state = 0;
        }
        else
        {
            if(agent.remainingDistance > 2.0f)
            {
                state = 1;
            }
            else
            {
                state = 2;
            }

            
        }

        return state;
    }

    void Idle()
    {
        waitTime -= Time.deltaTime;
        if(waitTime <= 0)
        {
            state = 0; // ���¸� patrol�� �ٲ۴�
        }
    }

    void SetPatrols()
    {
        agent.isStopped = false;
        //state = 0;
        if (agent.remainingDistance < 0.2f)
        {
            num++;
            num %= destination.childCount;

            agent.SetDestination(destination.GetChild(num).position);
         //targetVector = destinations[i].position - transform.position;
        }
    }

    private void Attack()
    {
        agent.isStopped = true;
    }

    void Chase()
    {
        agent.isStopped = false;
        agent.SetDestination(targetPosition);
        
    }

    bool FindPlayer()
    {
        bool result = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, findRange, LayerMask.GetMask("Player"));
        
        if(colliders.Length >0)
        {
            targetPosition = colliders[0].transform.position;
            result = true;
        }
        

        return result;
    }

    //1. �ֺ� �±� Ž��
    //2. Ư�� �±� �߽߰� �ش� �±׷� �̵�
    //3. �ش� �±װ� �ʹ� �־����� �ٽ� ���� �������� �̵�

}
