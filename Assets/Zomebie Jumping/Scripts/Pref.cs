using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pref
{
    public static int bestScore
    {
        set
        {
            int oldScore = PlayerPrefs.GetInt(PrefkKey.BestScore.ToString(), 0);

            if(value > oldScore || oldScore == 0)
            {
                PlayerPrefs.SetInt(PrefkKey.BestScore.ToString(), value);
            }
        }
        get => PlayerPrefs.GetInt(PrefkKey.BestScore.ToString(), 0);
    }
}
