using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mouse_Control : MonoBehaviour
{
    Vector3 mousePos;
    Vector3 transPos;

    //public GameObject TowerPositon = null;

    // Update is called once per frame
    public GameObject Tower = null;
    public GameObject Tower2 = null;

    private bool WallState=false;
    private bool TowerZone = false;

    GameObject[] ChildObejct;

    int TowerNumber = 0;

    public Camera MainCamera;

    Collider Mycollider;
    
    private void Awake()
    {
        ChildObejct = new GameObject[transform.childCount];
        Mycollider = GetComponent<Collider>();
        for(int i = 0; i < ChildObejct.Length; i++)
        {
            ChildObejct[i] = transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        
        //mousePos = UnityEngine.Input.mousePosition;
        mousePos = Mouse.current.position.ReadValue();
        //transPos = Camera.main.ScreenToWorldPoint(mousePos)
        //transform.position = new Vector3(transPos.x, 3.0f, transPos.z);
        mousePos.z = Camera.main.farClipPlane;
        //mousePos.y = 3.0f;
       
        //transform.position = new Vector3(transPos.x, 3.0f, transPos.y);
      
        //TowerPositon.transform.position = new Vector3(transform.position.x, 3.0f, transform.position.z);
        Keyboard k = Keyboard.current;

        

        Ray cameraRay = MainCamera.ScreenPointToRay(mousePos);
        Plane GroupPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if (GroupPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointTolook = cameraRay.GetPoint(rayLength);   
            pointTolook.y = 5.5f;
            transform.position = pointTolook;

        }

        if (k.aKey.wasPressedThisFrame && WallState && !TowerZone)
        {
            if (TowerNumber == 0)
            {
                GameObject T = Instantiate(Tower);
                T.transform.position = new Vector3(transform.position.x, 8.5f, transform.position.z);
            }
            else if (TowerNumber == 1)
            {
                GameObject T = Instantiate(Tower2);
                T.transform.position = new Vector3(transform.position.x, 8.5f, transform.position.z);
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            //Debug.Log("벽위에있음");
            WallState = true;
            

        }

        if(other.CompareTag("TowerSpawnRange"))
        {
            //Debug.Log("타워랑겹침");
            TowerZone = true;
        }
        //Debug.Log("벽위에있음");
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
