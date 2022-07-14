using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
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

    private void Awake()
    {
        ChildObejct = new GameObject[transform.childCount];
        for(int i = 0; i < ChildObejct.Length; i++)
        {
            ChildObejct[i] = transform.GetChild(i).gameObject;
        }
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        //transPos = Camera.main.ScreenToWorldPoint(mousePos)
        //transform.position = new Vector3(transPos.x, 3.0f, transPos.z);
        mousePos.z = Camera.main.farClipPlane;
        //mousePos.y = 3.0f;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        //transform.position = new Vector3(transPos.x, 3.0f, transPos.y);
        transform.position= transPos;
        //TowerPositon.transform.position = new Vector3(transform.position.x, 3.0f, transform.position.z);

        if (Input.GetKeyDown(KeyCode.A) && WallState && !TowerZone)
        {
            if (TowerNumber == 0)
            {
                GameObject T = Instantiate(Tower);
                T.transform.position = new Vector3(transform.position.x, 3.0f, transform.position.z);
            }else if(TowerNumber==1)
            {
                GameObject T = Instantiate(Tower2);
                T.transform.position = new Vector3(transform.position.x, 3.0f, transform.position.z);
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
