using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Archer : MonoBehaviour
{
    //화살 타워에 들어가는 스크립트

    public GameObject bullet = null;
    public float BulletSpeed = 10.0f;

    protected float BulletDelayMax = 1.0f;
    private float BulletDelay = 0.0f;

    public Transform BulletPoint = null;

    private Queue<GameObject> EnemyQueue = new Queue<GameObject>();
    GameObject target = null;

    bool isAttack = false;
    Animator anime;

    //미니맵용
    private Vector3 quadPosition;
    Transform quad;


    GameObject towerLine;

    SphereCollider spherCollider;
    float towerRange = 0.0f;
    
    private void Awake()
    {
        anime = GetComponent<Animator>();
        quad = transform.Find("Tower_Quad");
        towerLine = transform.Find("Tower_Line").gameObject;
        spherCollider=GetComponent<SphereCollider>();
        towerRange = spherCollider.radius * 40;
       
    }

    private void Start()
    {
        GameManager.INSTANCE.towerSwapDelegate += LayerChange;
    }
    /// <summary>
    /// 타워설치모드가 아닌 전투모드일때만 행동
    /// </summary>
    private void FixedUpdate()
    {


        if (!GameManager.INSTANCE.CAMERASWAP)
        {
            if (BulletDelay < BulletDelayMax && !isAttack) 
            {
                BulletDelay += Time.fixedDeltaTime;
            }

            //EnemyQueue에 enemy가 있고 타겟이 없을 때 EnemyQueue에서 타겟에 할당
            if (EnemyQueue.Count > 0 && target == null) 
            {

                target = EnemyQueue.Dequeue();
                if (target.activeInHierarchy == false) //EnemyQueue에 있는 enemy가 이미 죽었다면 타겟을 null로 만듬
                {
                    target = null;
                }
            }


            //타겟이 존재하면 타겟을 바라봄
            if (target != null) 
            {
                Vector3 LookDir = (target.transform.position - transform.position).normalized;
                LookDir.y = 0;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(LookDir), Time.fixedDeltaTime * 10.0f);

                if (target.activeInHierarchy == false || (target.transform.position - transform.position).sqrMagnitude > towerRange)
                {
                    //Debug.Log((target.transform.position - transform.position).sqrMagnitude);
                    target = null;
                }

            }
            //타겟이 존재하면서 딜레이가 딜레이Max를 넘어가면 공격애니메이션 활성
            if (BulletDelay > BulletDelayMax && target != null)
            {
                isAttack = true;
                anime.SetTrigger("Attack");
                BulletDelay = 0.0f;

            }

            quadPosition = new Vector3(quad.position.x, transform.position.y, quad.position.z); //미니맵고정용
            quad.transform.LookAt(quadPosition); //미니맵고정용

            
        }
    }

    protected void LayerChange()
    {
        
        if(!GameManager.INSTANCE.CAMERASWAP)
        {
            int temp = LayerMask.NameToLayer("Player_Quad");
            quad.gameObject.layer = temp;
            towerLine.layer = temp;

        }else
        {
            int temp = LayerMask.NameToLayer("Tower_Quad");
            quad.gameObject.layer = temp;
            towerLine.layer = temp;

        }
    }
    /// <summary>
    /// 할당되있는 불렛을 생성하여 타겟방향을 발사하는 함수
    /// </summary>
    public void Attack()
    {

        if (target != null)
        {
            GameObject b = Instantiate(bullet);
            b.transform.position = BulletPoint.transform.position;
            Vector3 dir = (target.transform.position - BulletPoint.transform.position).normalized;
            Rigidbody BulletRigid = b.GetComponent<Rigidbody>();
            BulletRigid.transform.LookAt(target.transform.position);
            dir.y += 0.1f;
            BulletRigid.velocity = dir * BulletSpeed;
        }
        isAttack = false;
    }

    //범위에 들어온 적들을 queue에 넣는다
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

            EnemyQueue.Enqueue(other.gameObject);

        }
    }
}
