using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Archer : MonoBehaviour
{
    //ȭ�� Ÿ���� ���� ��ũ��Ʈ

    public GameObject bullet = null;
    public float BulletSpeed = 10.0f;

    protected float BulletDelayMax = 1.0f;
    private float BulletDelay = 0.0f;

    public Transform BulletPoint = null;

    private Queue<GameObject> EnemyQueue = new Queue<GameObject>();
    GameObject target = null;

    bool isAttack = false;
    Animator anime;

    private void Awake()
    {
        anime = GetComponent<Animator>();
    }
    /// <summary>
    /// Ÿ����ġ��尡 �ƴ� ��������϶��� �ൿ
    /// </summary>
    private void FixedUpdate()
    {


        if (!GameManager.INSTANCE.CAMERASWAP)
        {
            if (BulletDelay < BulletDelayMax && !isAttack) 
            {
                BulletDelay += Time.fixedDeltaTime;
            }

            //EnemyQueue�� enemy�� �ְ� Ÿ���� ���� �� EnemyQueue���� Ÿ�ٿ� �Ҵ�
            if (EnemyQueue.Count > 0 && target == null) 
            {

                target = EnemyQueue.Dequeue();
                if (target.activeInHierarchy == false) //EnemyQueue�� �ִ� enemy�� �̹� �׾��ٸ� Ÿ���� null�� ����
                {
                    target = null;
                }
            }


            //Ÿ���� �����ϸ� Ÿ���� �ٶ�
            if (target != null) 
            {
                Vector3 LookDir = (target.transform.position - transform.position).normalized;
                LookDir.y = 0;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(LookDir), Time.fixedDeltaTime * 10.0f);

                if (target.activeInHierarchy == false || (target.transform.position - transform.position).sqrMagnitude > 280)
                {
                    target = null;
                }

            }
            //Ÿ���� �����ϸ鼭 �����̰� ������Max�� �Ѿ�� ���ݾִϸ��̼� Ȱ��
            if (BulletDelay > BulletDelayMax && target != null)
            {
                isAttack = true;
                anime.SetTrigger("Attack");
                BulletDelay = 0.0f;

            }


        }
    }
    /// <summary>
    /// �Ҵ���ִ� �ҷ��� �����Ͽ� Ÿ�ٹ����� �߻��ϴ� �Լ�
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
            BulletRigid.velocity = dir * BulletSpeed;
        }
        isAttack = false;
    }

    //������ ���� ������ queue�� �ִ´�
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {

            EnemyQueue.Enqueue(other.gameObject);

        }
    }
}
