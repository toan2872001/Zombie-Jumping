using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOverDialog : Dialog
{
    public Text totalScoreTxt;
    public Text bestScoreTxt;

    public override void Show(bool isShow)
    {
        base.Show(isShow);
        if (totalScoreTxt && GameManager.Ins)
            totalScoreTxt.text = GameManager.Ins.Score.ToString();

        if (bestScoreTxt)
            bestScoreTxt.text = Pref.bestScore.ToString();
    }
    private void OnsceneLoadedEvent(Scene scene ,LoadSceneMode mode)
    {
        if (GameManager.Ins)
            GameManager.Ins.PlayGame();
    }

    public void Replay()
    {
        SceneManager.sceneLoaded += OnsceneLoadedEvent;
        SceneController.Ins.LoadCurrentScene();
    }
}
