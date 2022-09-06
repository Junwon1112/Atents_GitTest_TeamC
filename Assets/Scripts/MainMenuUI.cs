using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    Button playButton;

    private void Awake()
    {
        playButton = transform.GetComponentInChildren<Button>();
    }

    private void Start()
    {
        ClickButton();
    }

    private void ClickButton()
    {
        playButton.onClick.AddListener(LoadingSceneLoad);
    }
    
    private void LoadingSceneLoad()
    {
        SceneManager.LoadScene("LoadingScene");
    }

}
