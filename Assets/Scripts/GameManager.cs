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
    ItemDataManager itemData;

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

    int maxWave = 2; //최대 웨이브수
    int wave = 1;  // 현재웨이브
    int stage = 1; // 현재 스태이지

    private GameObject[] StageText; //스테이지를 보여주는 텍스트를 저장하는 변수 타워설치와 전투모드때 위치가 다르기 때문에 배열로 받아옴
    private GameObject[] WaveText; //웨이브를 보여주는 텍스트를 저장하는 변수 타워설치와 전투모드때 위치가 다르기 때문에 배열로 받아옴

    private GameObject BossMasage; //보스등장 경고 메시지를 보여주는 게임오브젝트를 저장하는 변수

    InventoryUI inventoryUI;
    public InventoryUI InvenUI
    {
        get => inventoryUI;
    }

    public ItemDataManager ItemData
    {
        get => itemData;
    }

    public int MaxWave
    {
        get { return maxWave; }
    }
    public int Wave
    {
        get { return wave; }
        set { wave = value;
            WaveChange?.Invoke();
        
        }
    }

    public System.Action WaveChange; //wave가 변할때마다 실행시켜주는 델리게이트

    public int Stage
    {
        get { return stage; }
        set { stage = value;
        StageChange?.Invoke();
        }
    }

    public System.Action StageChange; //stage가 변할때만다 실행시켜주는 델리게이트 

    public int MONSTERLIVECOUNT
    {
        get { return monterLiveCount; }
        set
        {
            monterLiveCount = value;

            if (wave == maxWave)
            {

                if (monterLiveCount < 1)
                {
                    Cursor.lockState = CursorLockMode.None;
                    SceneManager.LoadScene(1);
                }
            }
            else
            {

                if (monterLiveCount < 2)
                {
                    TowerSwap();
                    Wave += 1;
                    Player.GetComponent<PlayerWolf>().MONEY += 500;
                }
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

            //DontDestroyOnLoad(gameObject);
            //SceneManager.sceneLoaded += OnSceneLoaded;
            Initialize();

            
            
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

        StageText = GameObject.FindGameObjectsWithTag("StageText");

        WaveText = GameObject.FindGameObjectsWithTag("WaveText");

        StageText[1].SetActive(false);
        WaveText[1].SetActive(false);

        BossMasage = GameObject.FindGameObjectWithTag("BossText");
        BossMasage.SetActive(false);

        itemData = GetComponent<ItemDataManager>();
        inventoryUI = FindObjectOfType<InventoryUI>();

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
        StageText[0].SetActive(CameraSwap);
        StageText[1].SetActive(!CameraSwap);
        WaveText[0].SetActive(CameraSwap);
        WaveText[1].SetActive(!CameraSwap);


        if(!CameraSwap)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }else
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }

    public IEnumerator BossMasageOn()
    {
        BossMasage.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        BossMasage.SetActive(false);
    }

}
