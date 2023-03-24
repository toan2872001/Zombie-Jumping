using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiManager : Singleton<GuiManager>
{
    public GameObject mainMenu;
    public GameObject gameplay;
    public Text textScore;
    public PauseDialog pauseDialog;
    public GameOverDialog gameoverDiaglog;

    public override void Awake()
    {
        MakeSingleton(false);
    }
    public void ShowGamePlay(bool isShow)
    {
        if (gameplay)
        {
            gameplay.SetActive(isShow);
        }
        if (mainMenu)
        {
            mainMenu.SetActive(!isShow);
        }
    }

    public void UpdateScore(int score)
    {
        if (textScore)
        {
            textScore.text = score.ToString();
        }
    }
}
