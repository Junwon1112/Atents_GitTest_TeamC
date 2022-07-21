using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public TestPlayer player;

    private void Awake()
    {
        player = GetComponent<TestPlayer>();

        if(instance==null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }else
        {
            if(instance!=this)
            {
                Destroy(gameObject);
            }
        }
    }
}
