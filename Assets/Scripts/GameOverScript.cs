using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    Animator animator;
    TextMeshProUGUI youDiedText;
    float maxAlpha = 1.0f;
    float alpha = 0.0f;

    Button reStartButton;
    Button mainMenuButton;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Die");
        youDiedText = GameObject.Find("YOUDIEDText").GetComponent<TextMeshProUGUI>();
        reStartButton = GameObject.Find("RestartButton").GetComponent<Button>();
        mainMenuButton = GameObject.Find("MainMenu").GetComponent<Button>();

        mainMenuButton.onClick.AddListener(MainMenuButton);
        mainMenuButton.gameObject.SetActive(false);
        reStartButton.onClick.AddListener(RestartButton);
        reStartButton.gameObject.SetActive(false);

        youDiedText.alpha = alpha;
        StartCoroutine(died());
    }


    IEnumerator died()
    {
        while(alpha<maxAlpha)
        {
            alpha += Time.deltaTime*0.2f;
            youDiedText.alpha = alpha;
            yield return null;
        }
        reStartButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
    }

    void RestartButton()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
