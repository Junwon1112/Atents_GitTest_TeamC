using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //게임의 흐름을 제어하고 미리오브젝트를 찾아주는 스크립트

    private static GameManager instance;

    private bool CameraSwap = true; //타워설치모드, 전투모드전환용 변수

    public GameObject TS = null; //마우스 오브젝트 변수
    public GameObject ButtonGroup; // 타워변경 버튼

    GameObject Mouse_Cotrol;

    private GameObject Player;
    private GameObject Player_Hp;

    private GameObject TopViewCamera;

    private GameObject MonsterSpawner;

    private GameObject MiniMap;

    GameObject StartButton;

    int monterLiveCount = 0;

    public int MONSTERLIVECOUNT
    {
        get { return monterLiveCount; }
        set
        {
            monterLiveCount = value;
            if(monterLiveCount<1)
            {
                TowerSwap();
                Player.GetComponent<PlayerWolf>().MONEY += 500;
            }
        }
    }
    public GameObject PLAYER
    {
        get { return Player; }
    }
    public bool CAMERASWAP
    {
        get { return CameraSwap; }
        set { CameraSwap = value; }
    }
    public static GameManager INSTANCE
    {
        get { return instance; }
    }

    public GameObject MOUSE
    {
        get { return Mouse_Cotrol; }
    }

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            
            
        }else
        {
            if(instance!=this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Initialize();
    }

    /// <summary>
    /// 처음 시작시 미리오브젝트를 찾고 비활성, 마우스커서 안보이게 해주는 함수
    /// </summary>
    private void Initialize()
    {
        Mouse_Cotrol = GameObject.FindGameObjectWithTag("Mouse_Control");
        Player = GameObject.FindGameObjectWithTag("Player");
        Player_Hp = GameObject.FindGameObjectWithTag("Player_Hp");
        TopViewCamera = GameObject.FindGameObjectWithTag("TopViewCamera");
        MonsterSpawner = GameObject.FindGameObjectWithTag("MonsterSpawner");
        MiniMap = GameObject.FindGameObjectWithTag("MiniMap");
        MiniMap.gameObject.SetActive(false);

        StartButton = GameObject.FindGameObjectWithTag("StartButton");
        StartButton.GetComponent<Button>().onClick.AddListener(TowerSwap);
        Cursor.visible = false;


    }
    /// <summary>
    /// 타워설치모드,전투모드를 스왑해주는 함수
    /// </summary>

    public void TowerSwap()
    {
        CameraSwap = !CameraSwap;
        TS.SetActive(CameraSwap);
        ButtonGroup.SetActive(CameraSwap);
        Player_Hp.SetActive(!CameraSwap);
        TopViewCamera.SetActive(CameraSwap);
        MiniMap.SetActive(!CameraSwap);
        StartButton.SetActive(CameraSwap);
        MonsterSpawner.GetComponent<MonsterSpawner>().StartSpawn(!CameraSwap);
        monterLiveCount = MonsterSpawner.GetComponent<MonsterSpawner>().maxMonsterCount;
        MonsterSpawner.GetComponent<MonsterSpawner>().monsterCount = 0;

        if(!CameraSwap)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }else
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }

}
