using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager
{
    #region HighScore
    public static bool HasHighScore(int _level)
    {
        return PlayerPrefs.HasKey("Level" + _level + "HighScore");
    }
    public static void  SaveHighScore(int _level , float _highscore)
    {
        PlayerPrefs.SetFloat("Level"+ _level + "HighScore" , _highscore);
    }

    public static float LoadHighScore(int _level)
    {
        return PlayerPrefs.GetFloat("Level" + _level + "HighSscore");
    }
    #endregion
}
