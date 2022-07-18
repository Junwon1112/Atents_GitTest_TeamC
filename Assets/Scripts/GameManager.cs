using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    PlayerWolf playerWolf;

    public PlayerWolf MainPlayer
    {
        get => playerWolf;
    }

    static GameManager instance = null;

    public static GameManager Inst
    {
        get => instance;
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Initialize();
    }

    private void Initialize()
    {
        playerWolf = FindObjectOfType<PlayerWolf>();
    }
}
