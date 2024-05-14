using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
    public TMP_Text scoreText;
    public GameObject menuUI;
    //public GameObject _UIinGame;
    public GameObject _UIendGame;

    private static UIManager ins;
    public static UIManager Ins => ins;

    private void Awake()
    {
        ins = this;
    }

    public void TurnOnPrize()
    {
        _UIendGame.SetActive(true);
    }

    public void TurnOffMenuUI()
    {
        menuUI.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    /*public void TurnOnlMenuUI()
    {
        _UIinGame.SetActive(true);
    }*/

    public void SetScoreText(string text)
    {
        if (scoreText != null)
        {
            scoreText.text = text;
        }
    }

    private void Start()
    {
        
    }
}
