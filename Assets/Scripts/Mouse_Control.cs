using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse_Control : MonoBehaviour
{
    //���콺�� �Ѿƴٴϸ鼭 ������ Ÿ���� ��ġ���ִ� ��ũ��Ʈ

    Vector3 mousePos;
    
    public GameObject Tower = null;
    public GameObject Tower2 = null;

    private bool WallState=false;
    private bool TowerZone = false;

    GameObject[] ChildObejct;

    int TowerNumber = 0;

    public Camera MainCamera;

    PlayerWolf player;

    int Tower1Price = 100;
    int Tower2Price = 200;
    
    private void Awake()
    {
        ChildObejct = new GameObject[transform.childCount];
        for(int i = 0; i < ChildObejct.Length; i++)
        {
            ChildObejct[i] = transform.GetChild(i).gameObject;
        }

        
    }

    private void Start()
    {
        player = GameManager.INSTANCE.PLAYER.GetComponent<PlayerWolf>();
    }

    /// <summary>
    /// ���콺�� ��ġ�� �޾ƿ��� aŰ�� �������� ����+�ٸ�Ÿ���� ��ó�� ���� ���� ������ ��ġ
    /// </summary>
    void Update()
    {
        
        mousePos = Mouse.current.position.ReadValue();

        mousePos.z = Camera.main.farClipPlane;
      
        Keyboard k = Keyboard.current;

        

        Ray cameraRay = MainCamera.ScreenPointToRay(mousePos);
        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if (GroupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);   
            pointTolook.y = 3.5f;
            transform.position = pointTolook;

        }

        if (k.aKey.wasPressedThisFrame && WallState && !TowerZone)
        {
            
            if (TowerNumber == 0 && player.MONEY>=Tower1Price)
            {
                GameObject T = Instantiate(Tower);
                T.transform.position = new Vector3(transform.position.x, 6.5f, transform.position.z);
                player.MONEY -= Tower1Price;
            }
            else if (TowerNumber == 1 && player.MONEY>=Tower2Price)
            {
                GameObject T = Instantiate(Tower2);
                T.transform.position = new Vector3(transform.position.x, 6.5f, transform.position.z);
                player.MONEY -= Tower2Price;
            }
        }

    }

    

    private void OnTriggerStay(Collider other)
    {
        //������ ������ WallState�� true�� ���� ��ġ�� �� �ְ�����
        if (other.CompareTag("Wall"))
        {
            
            WallState = true;
            

        }

        //�ٸ� Ÿ����ó�� ������ TowerZone�� true�� ���� ��ġ�� ���ϰ���
        if(other.CompareTag("TowerSpawnRange"))
        {
            
            TowerZone = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Wall"))
        {
            WallState = false;
        }

        if (other.CompareTag("TowerSpawnRange"))
        {
            
            TowerZone = false;
        }
    }

    /// <summary>
    /// ��ư�� ������ ��� ������ ��ư�� �����ϰ� �������� ��Ȱ��ȭ �ؼ� ������ ��ư�� ������Ʈ�� ���̰��ϴ� �Լ�
    /// </summary>
    /// <param name="number">Ȱ��ȭ �ؾ��ϴ� Ÿ���� ��ȣ</param>
    public void ObjectSwap(int number)
    {
        for (int i = 0; i < ChildObejct.Length; i++)
        {
            ChildObejct[i].SetActive(false);
        }
        TowerNumber = number;
        ChildObejct[number].SetActive(true);
    }
}
