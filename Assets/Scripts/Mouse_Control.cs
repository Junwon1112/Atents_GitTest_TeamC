using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse_Control : MonoBehaviour
{
    //마우스를 쫓아다니면서 벽위에 타워를 설치해주는 스크립트

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
    /// 마우스의 위치를 받아오고 a키를 눌렀을때 벽위+다른타워가 근처에 없고 돈이 있으면 설치
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
        //벽위에 있으면 WallState를 true로 만들어서 설치할 수 있게해줌
        if (other.CompareTag("Wall"))
        {
            
            WallState = true;
            

        }

        //다른 타워근처에 있으면 TowerZone을 true로 만들어서 설치를 못하게함
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
    /// 버튼을 눌렀을 경우 선택한 버튼을 제외하고 나머지를 비활성화 해서 선택한 버튼의 오브젝트만 보이게하는 함수
    /// </summary>
    /// <param name="number">활성화 해야하는 타워의 번호</param>
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
