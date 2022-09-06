using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class LoadingUI : MonoBehaviour
{
    //1. loading...만들기
    //2. 로딩지남에 따라 로딩바 이동, 완료되면 로딩 씬이동
    Player_Wolf actions;

    TextMeshProUGUI loadingText;

    const int MAXNUM = 10000;
    string temp = "Loading";

    Slider loadingBar;

    AsyncOperation async;

    float loadingProgress = 0.0f;

    IEnumerator PrintLoadingText;

    bool loadingCompleted = false;

    private void Awake()
    {
        loadingText = transform.Find("LoadingText").GetComponent<TextMeshProUGUI>();
        loadingBar = GetComponentInChildren<Slider>();
        actions = new Player_Wolf();
    }

    private void OnEnable()
    {
        actions.CustomUI.Enable();
        actions.CustomUI.Click.performed += ClickScreen;
    }

    

    private void OnDisable()
    {
        actions.CustomUI.Click.performed -= ClickScreen;
        actions.CustomUI.Disable();
    }

    private void Start()
    {
        PrintLoadingText = PrintLoading();
        StartCoroutine(PrintLoadingText);

        StartCoroutine(LoadScene());

    }

    private void ClickScreen(InputAction.CallbackContext obj)
    {
        if (loadingCompleted)
        {
            async.allowSceneActivation = true;
        }
    }


    IEnumerator PrintLoading()
    {
        for(int i = 0; i < MAXNUM; i++)
        {
            loadingText.text = temp;
            temp += ".";
            i %= 4;

            if(i == 0)
            {
                temp = "Loading";
            }
            yield return new WaitForSeconds(0.2f);

        }
    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync("MainScene");
        async.allowSceneActivation = false;
        float tempNum;

        while(loadingProgress != 1.0)
        {
            tempNum = loadingProgress;
            loadingProgress = Mathf.Lerp(tempNum, async.progress + 0.1f, 0.1f);

            if (loadingProgress > 0.9f)
            {
                loadingProgress = 1.0f;
            }
            loadingBar.value = loadingProgress;

            

            yield return null;
        }


        loadingCompleted = true;
        StopCoroutine(PrintLoadingText);
        loadingText.text = "Please Click The Screen";


    }

}
