using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField] GameObject content;
    [SerializeField] TextMeshProUGUI killcountText;

    public void Show()
    {
        content.SetActive(true);
        killcountText.text = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().killCount.ToString();
    }

    public void Hide() 
    {
        content.SetActive(false);
    }

    public void RestartButtonClick()
    {
        FindObjectOfType<GameManager>().StartGame();
    }

    public void QuitButtonClick()
    {
        Application.Quit();
    }
}
