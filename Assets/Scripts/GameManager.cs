using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    ItemDataManager itemData;

    private bool CameraSwap = true;

    public GameObject TS = null;
    public GameObject ButtonGroup;

    GameObject Mouse_Cotrol;

    private GameObject Player;
    private GameObject Player_Hp;

    public GameObject PLAYER
    {
        get { return Player; }
    }

    PlayerWolf player;
    public PlayerWolf MainPlayer
    {
        get => player;
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

    InventoryUI inventoryUI;
    public InventoryUI InvenUI => inventoryUI;

    public ItemDataManager ItemData
    {
        get => itemData;
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
    
    private void Initialize()
    {
        Mouse_Cotrol = GameObject.FindGameObjectWithTag("Mouse_Control");
        Player = GameObject.FindGameObjectWithTag("Player");
        Player_Hp = GameObject.FindGameObjectWithTag("Player_Hp");
        itemData = GetComponent<ItemDataManager>();
        inventoryUI = FindObjectOfType<InventoryUI>();
        player = FindObjectOfType<PlayerWolf>();
    }

    public void TowerSwap()
    {
        CameraSwap = !CameraSwap;
        TS.SetActive(CameraSwap);
        ButtonGroup.SetActive(CameraSwap);
        Player_Hp.SetActive(!CameraSwap);
    }

}
