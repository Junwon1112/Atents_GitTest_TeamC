using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class LoadingScript : MonoBehaviour
{
    string nextSceneName = "MainScene";

    AsyncOperation async;

    Player_Wolf inputActions;
    float loadRatio = 0.0f;
    bool loadCompleted=false;

    IEnumerator loadingTextCoroutine;
    IEnumerator loadSceneCoroutine;

    string[] loadTexts = new string[] { "Loading.", "Loading..", "Loading...", "Loading....", "Loading....." };
    TextMeshProUGUI loadingText;

    private void Awake()
    {
        inputActions = new Player_Wolf();

        loadingText = GetComponent<TextMeshProUGUI>();
        
    }

    private void Start()
    {
        loadingTextCoroutine = LoadingText();
        StartCoroutine(loadingTextCoroutine);
        loadSceneCoroutine = LoadScene();
        StartCoroutine(loadSceneCoroutine);
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Click.performed += MousePress;
    }

    private void OnDisable()
    {
        inputActions.UI.Click.performed -= MousePress;
        inputActions.UI.Disable();
    }

    private void MousePress(InputAction.CallbackContext obj)
    {
        if(loadCompleted)
        {
            async.allowSceneActivation = true;
        }
    }

    

    IEnumerator LoadingText()
    {
        int index = 0;
        while(true)
        {
            loadingText.text = loadTexts[index];
            index++;
            if(index>loadTexts.Length-1)
            {
                index = 0;
            }

            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator LoadScene()
    {
        async = SceneManager.LoadSceneAsync(nextSceneName);
        async.allowSceneActivation = false;

        while (loadRatio<1.0f)
        {
            loadRatio = async.progress + 0.1f;

            yield return null;
        }

        loadCompleted = true;
        Debug.Log("Load Complete");

        yield return new WaitForSeconds(1.0f);
        StopCoroutine(loadingTextCoroutine);
        loadingText.text = "Loading Complete. \nPress Button";
    }
}
